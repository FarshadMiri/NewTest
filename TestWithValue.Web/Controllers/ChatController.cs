using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using System.Globalization;
using Microsoft.AspNetCore.SignalR;
using TestWithValue.Domain.Enitities;
using TestWithValue.Web.Hubs.HubSupport;
using TestWithValue.Domain.ViewModels.Task;
namespace TestWithValue.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ITaskService _taskService;
        private readonly IHubContext<SupportHub> _hubContext;
        public ChatController(ITicketService ticketService, ITaskService taskService, IHubContext<SupportHub> hubContext)
        {
            _ticketService = ticketService;
            _taskService = taskService;
            _hubContext = hubContext;
        }
        public IActionResult Index()
        {
            return View(); // بازگشت به ویو index
        }

        [HttpGet]
        public IActionResult CheckUserRole()
        {
            if (User.IsInRole("User"))
            {
                return Json(new { isLoggedIn = true, role = "User" });
            }
            else if (User.IsInRole("Agent"))
            {
                return Json(new { isLoggedIn = true, role = "Agent" });
            }

            return Json(new { isLoggedIn = false });
        }
        public async Task<IActionResult> SupportChat(string? ticketId)
        {
            // بررسی نقش پشتیبان
            if (User.IsInRole("Agent"))
            {
                // اگر ticketId مقدار داشته باشد


                // دریافت لیست تمام تیکت‌ها
                var tickets = await _ticketService.GetAllTicketsAsync();
                return View(tickets); // ارسال لیست تیکت‌ها به ویو
            }

            // در صورت عدم نقش صحیح، به صفحه ورود هدایت شود
            return RedirectToAction("Login", "Auth");
        }
       
        [Authorize(Roles = "Agent")]
        public IActionResult SupportRequests()
        {
            if (User.IsInRole("Agent"))
            {



                return View(); // ارسال لیست تیکت‌ها به ویو
            }

            // در صورت عدم نقش صحیح، به صفحه ورود هدایت شود
            return RedirectToAction("Login", "Auth");
        }

        public IActionResult Tasks()
        {
            return View();
        }

        [HttpGet("tasks/gettasks")]
        public async Task<IActionResult> GetTasks(DateOnly date)
        {
            var tasks = await _taskService.GetTasksByDateAsync(date);

            if (tasks == null || !tasks.Any())
            {
                return Json(new { message = "هیچ وظیفه‌ای برای این تاریخ وجود ندارد." });
            }

            var result = tasks.Select(t => new
            {
                taskId = t.TaskId,
                title = t.Title,
                isDone = t.IsDone,
                date = t.TaskDate.ToString("yyyy-MM-dd"),
                startTime = t.TaskStartTime.HasValue ? t.TaskStartTime.Value.ToString("HH:mm") : null, // ساعت شروع
                endTime = t.TaskEndTime.HasValue ? t.TaskEndTime.Value.ToString("HH:mm") : null // ساعت پایان
            });

            return Json(result);
        }

        [HttpPost("tasks/updatetaskstatus")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] TaskStatusUpdateRequest request)
        {
            if (request.TaskId <= 0)
                return BadRequest("Invalid TaskId");

            await _taskService.UpdateTaskStatusAsync(request.TaskId, request.IsDone);
            return Ok(new { message = "Task status updated successfully" });
        }
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            var taskViewModels = tasks.Select(t => new TaskViewModel
            {
                TaskId = t.TaskId,
                Title = t.Title,
                IsDone = t.IsDone,
                TaskDate = t.TaskDate,
                TaskStartTime = t.TaskStartTime, // اضافه کردن ساعت شروع
                TaskEndTime = t.TaskEndTime // اضافه کردن ساعت پایان
            });

            return Json(taskViewModels); // بازگرداندن داده‌ها به صورت JSON برای استفاده در فرانت‌اند
        }
        [HttpGet]
        public async Task<IActionResult> GetTaskMessage(int id)
        {
            // دریافت پیام‌های مرتبط با TaskId از سرویس
            var messages = await _taskService.GetMessagesByTicketIdAsync(id);

            if (messages != null && messages.Any())
            {
                var response = messages.Select(msg => new
                {
                    senderId = msg.SenderId, // فرستنده پیام
                    message = msg.Message,  // متن پیام
                    sentAt = msg.SentAt  // زمان ارسال پیام
                });

                return Json(response); // بازگرداندن پیام‌ها به صورت JSON
            }

            return NotFound(new { message = "No messages found for this task." });
        }
    }
}
