using DinkToPdf;
using DinkToPdf.Contracts;
using iText.Commons.Actions.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
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


        [HttpPost("task/downloadpdf")]
        public async Task<IActionResult> DownloadPdf(int taskId)
        {
            try
            {
                // دریافت پیام‌های مربوط به taskId
                var messages = await _taskService.GetMessagesByTicketIdAsync(taskId);
                if (messages == null || !messages.Any())
                {
                    return BadRequest("هیچ پیامی برای این تسک وجود ندارد.");
                }

                // محتوای HTML برای تولید PDF
                var htmlContent = @"
        <!DOCTYPE html>
        <html lang='fa' dir='rtl'>
        <head>
            <meta charset='UTF-8'>
            <style>
                body {
                    font-family: 'Tahoma', sans-serif;
                    direction: rtl;
                    text-align: right;
                }
                .message {
                    margin-bottom: 20px;
                    border: 1px solid #ddd;
                    padding: 10px;
                }
            </style>
        </head>
        <body>
            <h1>پیام‌های تسک</h1>";

                foreach (var message in messages)
                {
                    htmlContent += $@"
            <div class='message'>
                <strong>فرستنده:</strong> {message.SenderId}<br />
                <strong>پیام:</strong> {message.Message}<br />
                <strong>تاریخ ارسال:</strong> {message.SentAt:yyyy-MM-dd HH:mm}
            </div>";
                }

                htmlContent += @"
        </body>
        </html>";

                // تنظیمات تولید PDF
                var pdfDoc = new HtmlToPdfDocument
                {
                    GlobalSettings = new GlobalSettings
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings { Top = 10, Bottom = 10 }
                    }
                };

                pdfDoc.Objects.Add(new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = htmlContent,
                    WebSettings = new WebSettings
                    {
                        DefaultEncoding = "utf-8"
                    }
                });

                // تولید PDF
                var pdf = _converter.Convert(pdfDoc); // ممکن است خطا اینجا باشد

                // بازگشت فایل PDF برای دانلود
                return File(pdf, "application/pdf", "task_messages.pdf");
            }
            catch (Exception ex)
            {
                // لاگ کردن خطا
                _logger.LogError(ex, "خطایی در تولید PDF رخ داده است.");
                return StatusCode(500, "خطایی در تولید PDF رخ داده است.");
            }
        }

    }
}

