using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;

namespace TestWithValue.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ITicketService _ticketService;
        public TaskController(ITaskService taskService, ITicketService ticketService)
        {
            _taskService = taskService;
            _ticketService = ticketService;
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


    }
}

