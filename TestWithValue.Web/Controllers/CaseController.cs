using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.CaseViewModel;

namespace TestWithValue.Web.Controllers
{
    public class CaseController : Controller
    {
        private readonly ICaseService _caseService;
        private readonly ILocationService _locationService;
        private readonly UserManager<IdentityUser> _userManager;



        public CaseController(ICaseService caseService, ILocationService locationService, UserManager<IdentityUser> userManager)
        {
            _caseService = caseService;
            _locationService = locationService;
            _userManager = userManager;

        }

        // اکشن برای نمایش فرم ایجاد پرونده
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var locations = await _locationService.GetLocationsForDropdownAsync();
            var viewModel = new CaseViewModel
            {
                Locations = locations // لیست لوکیشن‌ها را به ویو مدل اختصاص می‌دهیم
            };
            return View(viewModel); // ویو مدل به ویو ارسال می‌شود
        }

        // اکشن برای ذخیره پرونده
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CaseViewModel model)
        {
            var userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                // گرفتن شیء لوکیشن از سرویس
                var location = await _locationService.GetLocationByIdAsync(model.LocationId);

                if (location == null)
                {
                    // اگر لوکیشن یافت نشد
                    ModelState.AddModelError("", "Location not found.");
                    return View(model);
                }

                var newCase = new Tbl_Case
                {
                    Title = model.Title,
                    CaseType = model.CaseType,
                    IsDone = model.IsDone,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    LocationId = model.LocationId, // ذخیره ID لوکیشن
                    LocationName = location.Name,  // ذخیره اسم لوکیشن
                    UserId = userId // کاربر فعلی
                };

                await _caseService.AddCaseAsync(newCase); // ذخیره پرونده جدید
                return RedirectToAction("UserCases", new { userId });
            }

            // در صورتی که فرم اشتباه پر شود، دوباره فرم را نمایش می‌دهیم
            var locations = await _locationService.GetLocationsForDropdownAsync();
            model.Locations = locations;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserCases()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            var cases = await _caseService.GetCasesByUserIdAsync(userId);
            return View(cases); // ارسال لیست پرونده‌ها به ویو
        }
    }
}
