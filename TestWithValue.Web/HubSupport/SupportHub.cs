
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Domain.ViewModels.Ticket;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using PdfSharpCore;
using PdfSharpCore.Fonts;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Web.HubSupport
{
    public class SupportHub : Hub
    {
        private readonly ITicketService _ticketService;
        private readonly ITaskService _taskService;
        public SupportHub(ITicketService ticketService, ITaskService taskService)
        {
            _ticketService = ticketService;
            _taskService = taskService; 
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // بررسی اینکه آیا کاربر یا پشتیبان است
            if (Context.User.IsInRole("Agent"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Agent");
            }
            else if (Context.User.IsInRole("User"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "User");

                // دریافت ticketId برای کاربر
                var ticket = await _ticketService.GetOpenTicketForUserAsync(userId);

                if (ticket != null)
                {
                    // ارسال ticketId به کاربر
                    await Clients.Caller.SendAsync("ReceiveTicketId", ticket.Id);
                    // اضافه کردن کاربر به گروه مربوط به ticketId
                    await Groups.AddToGroupAsync(Context.ConnectionId, ticket.Id.ToString());
                }
                else
                {
                    // اگر تیکت وجود نداشت
                    await Clients.Caller.SendAsync("ReceiveTicketId", null);
                }
            }

            await base.OnConnectedAsync();
        }
        public async Task AddTask(string userId, string title, DateTime taskDate)
        {
            // بررسی اینکه آیا کاربر احراز هویت شده است یا خیر
            var currentUserId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // اگر کاربر احراز هویت نشده باشد، نمی‌توان وظیفه را ایجاد کرد
            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new HubException("کاربر احراز هویت نشده است.");
            }
            // بررسی اینکه آیا تیکتی برای این کاربر با این عنوان وجود دارد
            var existingTicket = await _ticketService.GetOpenTicketForUserByTitleAsync(userId, title);
            int ticketId;

            if (existingTicket == null)
            {
                // اگر تیکت موجود نیست، تیکت جدید ایجاد کنیم
                var newTicketModel = new TicketViewModel
                {
                    Title = title,
                    Description = "User has started a new conversation.",
                    UserId = userId
                };
                await _ticketService.CreateTicketAsync(newTicketModel);
                var ticket = await _ticketService.GetOpenTicketForUserByTitleAsync(userId, title);
                ticketId = ticket.Id;

                // اضافه کردن کاربر به گروه مربوط به ticketId
                await Groups.AddToGroupAsync(Context.ConnectionId, ticketId.ToString());
            }
            else
            {
                // اگر تیکت موجود است، از آن استفاده می‌کنیم
                ticketId = existingTicket.Id;
            }

            // ذخیره پیام کاربر با استفاده از ticketId

            // تبدیل DateTime به DateOnly برای ذخیره در دیتابیس
            var taskDateOnly = DateOnly.FromDateTime(taskDate);

            // ایجاد نمونه‌ای از وظیفه با استفاده از اطلاعات دریافتی
            var task = new Tbl_Task
            {
                TaskDate = taskDateOnly, // استفاده از DateOnly
                Title = title,           // تنظیم عنوان وظیفه
                IsDone = false,
                UserId = userId ,         // تنظیم UserId کاربر ایجادکننده وظیفه
                 TicketId=ticketId
                
            };

            // افزودن وظیفه به دیتابیس
            await _taskService.AddTaskAsync(task);

            // ارسال وظیفه جدید به گروه پشتیبان‌ها
            //await Clients.Group("Agent").SendAsync("ReceiveNewTask", task);
        }
        public async Task SendMessageToAgent(string userId, string title, string message)
        {
            // بررسی اینکه آیا تیکتی برای این کاربر با این عنوان وجود دارد
            var existingTicket = await _ticketService.GetOpenTicketForUserByTitleAsync(userId, title);
            int ticketId;

            if (existingTicket == null)
            {
                // اگر تیکت موجود نیست، تیکت جدید ایجاد کنیم
                var newTicketModel = new TicketViewModel
                {
                    Title = title,
                    Description = "User has started a new conversation.",
                    UserId = userId
                };
                await _ticketService.CreateTicketAsync(newTicketModel);
                var ticket = await _ticketService.GetOpenTicketForUserByTitleAsync(userId, title);
                ticketId = ticket.Id;

                // اضافه کردن کاربر به گروه مربوط به ticketId
                await Groups.AddToGroupAsync(Context.ConnectionId, ticketId.ToString());
            }
            else
            {
                // اگر تیکت موجود است، از آن استفاده می‌کنیم
                ticketId = existingTicket.Id;
            }

            // ذخیره پیام کاربر با استفاده از ticketId
            await _ticketService.SaveMessageAsync(ticketId, userId, message);

            // ارسال پیام به گروه پشتیبان‌ها
            await Clients.Group("Agent").SendAsync("ReceiveMessageFromUser", userId, message, ticketId, title);
        }

        public async Task SendMessageToUser(string userId, string ticketId, string message, string ticketTitle)
        {
            var agentId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (agentId == null)
            {
                throw new HubException("پشتیبان احراز هویت نشده است.");
            }

            var ticket = await _ticketService.GetOpenTicketForUserByTitleAsync(userId, ticketTitle);

            if (ticket == null || ticket.UserId != userId)
            {
                throw new HubException("تیکت یافت نشد یا متعلق به کاربر نیست.");
            }

            // ارسال پیام به گروه مربوط به ticketId
            await Clients.User(userId).SendAsync("ReceiveMessageFromAgent", agentId, message, ticketId);

            // ذخیره پیام در دیتابیس
            await _ticketService.SaveMessageAsync(Convert.ToInt32(ticketId), agentId, message);
        }

        public async Task SendEditToUser(string userId, string ticketId, string message)
        {
            try
            {
                // به‌روزرسانی پیام در پایگاه داده
                await _ticketService.UpdateMessageAsync(Convert.ToInt32(ticketId), message);

                // ارسال پیام به کلاینت
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendEditToUser: {ex.Message}");
            }
        }
    
        public async Task SendPdfToUser(string userId, string ticketId, string message)
        {
            var pdfPath = ConvertMessageToPdf(message, userId, ticketId);

            if (pdfPath != null)
            {
                await Clients.User(userId).SendAsync("ReceivePdf", pdfPath, ticketId);
            }
            else
            {
                Console.WriteLine("Failed to send PDF. File path is null.");
            }
        }

        public string ConvertMessageToPdf(string message, string userId, string ticketId)
        {
            string pdfPath = null;
            try
            {
                string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Editpdf");

                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                string pdfFileName = $"message_{userId}_{ticketId}.pdf";
                pdfPath = Path.Combine(rootPath, pdfFileName);

                using (var document = new PdfDocument())
                {
                    var page = document.AddPage();
                    page.Size = PageSize.A4;

                    using (var gfx = XGraphics.FromPdfPage(page))
                    {
                        string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Fonts","B-NAZANIN.TTF");
                        var fontResolver = GlobalFontSettings.FontResolver; // تنظیمات فونت برای PdfSharpCore
                       XFont font = new XFont("B Nazanin", 12, XFontStyle.Regular);

                        double margin = 20;
                        double y = margin;

                        // پردازش متن فارسی: معکوس کردن ترتیب کلمات برای نمایش درست در PdfSharp
                        string processedMessage = ReverseTextForPdfSharp(message);

                        // تقسیم متن به خطوط با عرض محدود
                        var lines = WrapTextToFit(gfx, processedMessage, font, page.Width - 2 * margin);
                        foreach (var line in lines)
                        {
                            gfx.DrawString(line, font, XBrushes.Black, new XPoint(page.Width - margin, y), XStringFormats.TopRight);
                            y += font.GetHeight();
                        }

                        document.Save(pdfPath);
                    }
                }

                return $"/Editpdf/{pdfFileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating PDF: {ex.Message}");
                return null;
            }
        }

        private string ReverseTextForPdfSharp(string input)
        {
            // معکوس کردن کلمات و ترتیب جمله‌ها
            var words = input.Split(' ');
            Array.Reverse(words);
            return string.Join(" ", words);
        }
        // تابع برای تقسیم متن به خطوط با توجه به عرض صفحه
        private List<string> WrapTextToFit(XGraphics gfx, string text, XFont font, double maxWidth)
        {
            var lines = new List<string>();
            var words = Regex.Split(text, @"\s+"); // تقسیم متن به کلمات
            var currentLine = "";

            foreach (var word in words)
            {
                var testLine = currentLine + (currentLine.Length > 0 ? " " : "") + word;
                var size = gfx.MeasureString(testLine, font);
                if (size.Width > maxWidth) // اگر خط بیشتر از حداکثر عرض باشد
                {
                    lines.Add(currentLine); // خط فعلی را اضافه کنید
                    currentLine = word; // خط جدید با کلمه جدید شروع کنید
                }
                else
                {
                    currentLine = testLine; // ادامه‌ی خط
                }
            }

            if (currentLine.Length > 0) // افزودن آخرین خط
            {
                lines.Add(currentLine);
            }

            return lines;
        }        // تابع برای معکوس کردن متن
        [HttpGet]
        public async Task LoadMessagesForTicket(string ticketId)
        {
            var messages = await _ticketService.GetMessagesByTicketIdAsync(Convert.ToInt32(ticketId));
            await Clients.Caller.SendAsync("ReceiveMessages", messages, ticketId);
        }

        public async Task<object> CloseTicket(string ticketId)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(Convert.ToInt32(ticketId));

            if (ticket == null)
            {
                return new { success = false, message = "تیکت پیدا نشد." };
            }

            if (ticket.IsClosed)
            {
                return new { success = false, message = "این تیکت قبلاً بسته شده است." };
            }

            // بستن تیکت
            ticket.IsClosed = true;
            await _ticketService.CloseTicketAsync(ticket.Id);

            return new { success = true, message = "تیکت با موفقیت بسته شد." };
        }


    }
}
