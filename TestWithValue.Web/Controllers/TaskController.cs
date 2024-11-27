using DinkToPdf;
using DinkToPdf.Contracts;
using iText.Commons.Actions.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using System.Security.Claims;
using System.Text.RegularExpressions;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Task;

namespace TestWithValue.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ITicketService _ticketService;
        private readonly IConverter _converter;
        private readonly ILogger<TaskController> _logger;
        public TaskController(ITaskService taskService, ITicketService ticketService, IConverter converter, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _ticketService = ticketService;
            _converter = converter;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ShowRequest()
        {
            // دریافت شناسه کاربر لاگین‌شده از کلایم‌ها
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // بررسی اینکه userId خالی نباشد
            if (string.IsNullOrEmpty(userId))
            {
                // اگر کاربر لاگین نکرده باشد، ریدایرکت به صفحه لاگین
                return RedirectToAction("Login", "Auth");
            }

            // دریافت taskهای کاربر
            var tasks = await _taskService.GetTasksByUserIdAsync(userId);

            // ارسال لیست taskها به ویو
            return View(tasks);
        }
        [HttpGet("task/gettaskmessages")]
        public async Task<IActionResult> GetTaskMessages(int taskId)
        {
            var messages = await _taskService.GetMessagesByTicketIdAsync(taskId);
            if (messages == null || !messages.Any())
            {
                return Json(new { messages = new List<object>(), isDone = false });
            }

            var task = await _taskService.GetTaskByIdAsync(taskId);
            var isDone = task?.IsDone ?? false;

            return Json(new { messages, isDone });
        }
        // اکشن برای نمایش پیام‌های تسک خاص
        [Route("Task/ViewTaskMessages/{taskId}")]
        public async Task<IActionResult> ViewTaskMessages(int taskId)
        {
            if (taskId <= 0)  // بررسی برای taskId نامعتبر
            {
                return NotFound();
            }

            var taskMessages = await _taskService.GetMessagesByTicketIdAsync(taskId);

            if (taskMessages == null || !taskMessages.Any())
            {
                return NotFound();
            }

            var viewModel = taskMessages.Select(msg => new TaskMessageViewModel
            {
                SenderId = msg.SenderId,
                Message = msg.Message,
                SentAt = msg.SentAt,
                TaskId = taskId  // اضافه کردن TaskId به ViewModel
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRequest([FromBody] EditRequestViewModel model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Messages))
                {
                    return Json(new { message = "Invalid input." });
                }

                // به‌روزرسانی پیام در سرویس
                await _taskService.UpdateMessageAsync(model.TaskId, model.Messages);

                return Json(new { message = "Changes saved successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditUserRequest: {ex.Message}");
                return Json(new { message = "Error saving changes." });
            }
        }


        [HttpPost("task/ConvertMessageToPdf")]
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
                    //page.Size = PageSize.A4;

                    using (var gfx = XGraphics.FromPdfPage(page))
                    {
                        string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Fonts", "B-NAZANIN.TTF");
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

        public IActionResult SupportTask()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] AddTaskDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Title) || model.TaskDate == null)
                return BadRequest("عنوان و تاریخ وظیفه باید مشخص شوند.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // دریافت UserId کاربر

            // تبدیل تاریخ از رشته به DateOnly
            if (!DateOnly.TryParse(model.TaskDate, out var taskDate))
                return BadRequest("تاریخ وارد شده معتبر نیست.");

            var task = new Tbl_Task
            {
                TaskDate = taskDate,
                Title = model.Title,
                IsDone = false,
                UserId = userId // کاربر ایجادکننده
            };

            await _taskService.AddTaskAsync(task);

            return Ok(new { Message = "وظیفه با موفقیت اضافه شد." });
        }


    }
}

