using iText.Commons.Actions.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Task;
using System.IO;
using iText.StyledXmlParser.Jsoup.Nodes;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;

namespace TestWithValue.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ITicketService _ticketService;
        //private readonly IConverter _converter;
        private readonly ILocationService  _locationService;

        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService taskService, ITicketService ticketService, /*IConverter converter,*/ ILogger<TaskController> logger, ILocationService locationService)
        {
            _taskService = taskService;
            _ticketService = ticketService;
            //_converter = converter;
            _logger = logger;
            _locationService = locationService; 
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


        //public async Task<IActionResult> DownloadPdf(int taskId)
        //{
        //    try
        //    {
        //        // دریافت پیام‌ها از دیتابیس
        //        var taskMessages = await _taskService.GetMessagesByTicketIdAsync(taskId);

        //        if (taskMessages == null || !taskMessages.Any())
        //        {
        //            return NotFound("هیچ پیامی برای این وظیفه موجود نیست.");
        //        }

        //        // مسیر ذخیره فایل PDF
        //        var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");
        //        if (!Directory.Exists(fileDirectory))
        //        {
        //            Directory.CreateDirectory(fileDirectory);
        //        }

        //        var filePath = Path.Combine(fileDirectory, $"task_{taskId}_messages.pdf");

        //        // ایجاد فایل PDF
        //        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            Document pdfDoc = new Document(PageSize.A4, 50, 50, 25, 25);
        //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        //            pdfDoc.Open();

        //            // بارگذاری فونت فارسی
        //            string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fonts", "Tahoma.ttf");
        //            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        //            Font font = new Font(bf, 12, Font.NORMAL);

        //            // افزودن پیام‌ها به PDF
        //            foreach (var message in taskMessages)
        //            {
        //                Paragraph paragraph = new Paragraph(message.Message, font)
        //                {
        //                    Alignment = Element.ALIGN_RIGHT
        //                };

        //                pdfDoc.Add(paragraph);
        //                pdfDoc.Add(new Paragraph("-----------------------------------", font)
        //                {
        //                    Alignment = Element.ALIGN_CENTER
        //                });
        //            }

        //            pdfDoc.Close();
        //            writer.Close();
        //        }

        //        // ارسال فایل به کاربر برای دانلود
        //        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        //        System.IO.File.Delete(filePath); // حذف فایل پس از دانلود

        //        return File(fileBytes, "application/pdf", Path.GetFileName(filePath));
        //    }
        //    catch (Exception ex)
        //    {
        //        // نمایش خطا
        //        return StatusCode(500, $"خطایی در تولید PDF رخ داده است: {ex.Message}");
        //    }
        //}        /// <summary>
        /// شکستن متن به خطوط با طول مشخص
        /// </summary>
        private List<string> SplitTextToLines(string text, int maxLength)
        {
            var words = text.Split(' ');
            var lines = new List<string>();
            var currentLine = "";

            foreach (var word in words)
            {
                if ((currentLine + word).Length > maxLength)
                {
                    lines.Add(currentLine.Trim());
                    currentLine = "";
                }
                currentLine += word + " ";
            }

            if (!string.IsNullOrWhiteSpace(currentLine))
            {
                lines.Add(currentLine.Trim());
            }

            return lines;
        }


        public async Task<IActionResult>  SupportTask()
        {
            var locations = await _locationService.GetLocationsForDropdownAsync(); // یا هر روش دیگری برای بارگذاری موقعیت‌ها

            var model = new SupportTaskViewModel
            {
                Locations = locations // مقداردهی موقعیت‌ها
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] AddTaskDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.TaskDate))
                return BadRequest("عنوان و تاریخ وظیفه باید مشخص شوند.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // تبدیل تاریخ شمسی به میلادی
            DateTime taskDateMiladi;
            try
            {
                var parts = model.TaskDate.Split('/');
                if (parts.Length != 3)
                    throw new FormatException("فرمت تاریخ صحیح نیست.");

                parts[0] = parts[0].Replace('۰', '0').Replace('۱', '1').Replace('۲', '2').Replace('۳', '3')
                    .Replace('۴', '4').Replace('۵', '5').Replace('۶', '6').Replace('۷', '7').Replace('۸', '8').Replace('۹', '9');
                parts[1] = parts[1].Replace('۰', '0').Replace('۱', '1').Replace('۲', '2').Replace('۳', '3')
                    .Replace('۴', '4').Replace('۵', '5').Replace('۶', '6').Replace('۷', '7').Replace('۸', '8').Replace('۹', '9');
                parts[2] = parts[2].Replace('۰', '0').Replace('۱', '1').Replace('۲', '2').Replace('۳', '3')
                    .Replace('۴', '4').Replace('۵', '5').Replace('۶', '6').Replace('۷', '7').Replace('۸', '8').Replace('۹', '9');

                int year = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int day = int.Parse(parts[2]);

                var persianCalendar = new System.Globalization.PersianCalendar();
                taskDateMiladi = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                return BadRequest("تاریخ وارد شده صحیح نیست: " + ex.Message);
            }

            TimeOnly? taskStartTime = null;
            TimeOnly? taskEndTime = null;

            if (!string.IsNullOrEmpty(model.TaskStartTime))
            {
                var startTimeParts = model.TaskStartTime.Split(':');
                int startHour = int.Parse(startTimeParts[0]);
                int startMinute = int.Parse(startTimeParts[1]);
                taskStartTime = new TimeOnly(startHour, startMinute);
            }

            if (!string.IsNullOrEmpty(model.TaskEndTime))
            {
                var endTimeParts = model.TaskEndTime.Split(':');
                int endHour = int.Parse(endTimeParts[0]);
                int endMinute = int.Parse(endTimeParts[1]);
                taskEndTime = new TimeOnly(endHour, endMinute);
            }

            // دریافت نام موقعیت از جدول Tbl_Location
            string locationName = string.Empty;
            if (model.LocationId.HasValue)
            {
                var location = await _locationService.GetLocationByIdAsync(model.LocationId.Value);
                if (location != null)
                {
                    locationName = location.Name;
                }
            }

            var task = new Tbl_Task
            {
                Title = model.Title,
                TaskDate = DateOnly.FromDateTime(taskDateMiladi),
                TaskDateString = model.TaskDate,
                TaskStartTime = taskStartTime,
                TaskEndTime = taskEndTime,
                UserId = userId,
                LocationId = model.LocationId, // ذخیره موقعیت مکانی
                LocationName = locationName, // ذخیره نام موقعیت مکانی
                IsDone = false
            };

            await _taskService.AddTaskAsync(task);

            return Ok(new { Message = "وظیفه با موفقیت اضافه شد." });
        }




    }
}

