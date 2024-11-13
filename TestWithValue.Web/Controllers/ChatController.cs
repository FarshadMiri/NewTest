using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using System.Globalization;
namespace TestWithValue.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ITaskService _taskService;  
        public ChatController(ITicketService ticketService, ITaskService taskService)
        {
            _ticketService = ticketService;
            _taskService = taskService;   
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
        public async Task<IActionResult> SupportChat()
        {
            // بررسی نقش پشتیبان
            if (User.IsInRole("Agent"))
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                return View(tickets); // ارسال لیست تیکت‌ها به ویو
                                      // بازگشت به ویو SupportChat
            }
            return RedirectToAction("Login", "Auth"); // در صورت عدم نقش صحیح، به صفحه ورود هدایت شود
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
                title = t.Title,
                isDone = t.IsDone,
                date = t.TaskDate.ToString("yyyy-MM-dd"), // تاریخ به صورت میلادی
                ticketId = t.TicketId // اضافه کردن TicketId
            });

            return Json(result);
        }
    }
}
