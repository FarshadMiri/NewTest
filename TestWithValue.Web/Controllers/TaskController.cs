using Microsoft.AspNetCore.Mvc;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;

namespace TestWithValue.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService; 
        public TaskController(ITaskService taskService)
        {
             _taskService = taskService;    
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
    }
}

