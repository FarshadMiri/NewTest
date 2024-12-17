using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace TestWithValue.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // متد اصلی داشبورد - بررسی نقش کاربر
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); // دریافت اطلاعات کاربر فعلی
            var userRole = await _userManager.GetRolesAsync(user); // دریافت نقش‌های کاربر

            // بررسی اینکه آیا کاربر دارای نقش "User" است یا "Agent"
            if (userRole.Contains("User"))
            {
                return RedirectToAction("UserDashboard");
            }
            else if (userRole.Contains("Agent"))
            {
                return RedirectToAction("AgentDashboard");
            }

            return RedirectToAction("AccessDenied", "Home"); // در صورتی که نقش مشخص نباشد
        }

        // متد داشبورد برای نقش "User"
        [Authorize(Roles = "User")]
        public IActionResult UserDashboard()
        {
            return View();
        }

        // متد برای هدایت به چت با پشتیبانی - فقط برای نقش "User"
        [Authorize(Roles = "User")]
        public IActionResult SupportChat()
        {
            return RedirectToAction("Index", "Home"); // صفحه چت که در HomeController متد Index دارد
        }
        [Authorize(Roles = "User")]
        public IActionResult CompleteInfo()
        {
            return RedirectToAction("CompleteUserInfo", "Auth"); // صفحه چت که در HomeController متد Index دارد
        }


        // متد برای صفحه درخواست‌نامه - فقط برای نقش "User"
        [Authorize(Roles = "User")]
        public IActionResult RequestForm()
        {
            return RedirectToAction("UserInfo","User");
        }

        // متد برای پیگیری درخواست‌ها - فقط برای نقش "User"
        [Authorize(Roles = "User")]
        public IActionResult RequestTracking()
        {
            return RedirectToAction("ShowRequest","Task");
        }

        // متد برای تشکیل پرونده - فقط برای نقش "User"
        [Authorize(Roles = "User")]
        public IActionResult CreateFile()
        {
            return RedirectToAction("Create","Case");
        }

        // متد برای لیست پرونده‌ها - فقط برای نقش "User"
        [Authorize(Roles = "User")]
        public IActionResult FileList()
        {
            return RedirectToAction("UserCases","Case");
        }

        // متدهای مربوط به داشبورد پشتیبان - فقط برای نقش "Agent"
        [Authorize(Roles = "Agent")]
        public IActionResult AgentDashboard()
        {
            return View("AgentDashboard"); // ویوی داشبورد پشتیبان
        }

        // متد درخواست‌های پشتیبان
        [Authorize(Roles = "Agent")]
        public IActionResult AgentRequestList()
        {
            // در اینجا می‌توانید کد برای نمایش درخواست‌های پشتیبان بنویسید
            return RedirectToAction("SupportRequests", "Chat");
        }

        // متد تعریف وظیفه
        [Authorize(Roles = "Agent")]
        public IActionResult DefineTask()
        {
            // در اینجا می‌توانید کد برای تعریف وظیفه‌ها بنویسید
            return RedirectToAction("SupportTask", "Task");
        }

        // متد لیست پرونده‌های پیشنهادی
        [Authorize(Roles = "Agent")]
        public IActionResult SuggestedFileList()
        {
            // در اینجا می‌توانید کد برای نمایش پرونده‌های پیشنهادی بنویسید
            return RedirectToAction("Index", "SuggestedCase");
        }

        // متد وظایف
        [Authorize(Roles = "Agent")]
        public IActionResult TasksList()
        {
            // در اینجا می‌توانید کد برای نمایش وظایف پشتیبان بنویسید
            return RedirectToAction("Tasks", "Chat");
        }
    }
}
