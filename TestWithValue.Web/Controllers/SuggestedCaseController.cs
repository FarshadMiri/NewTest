using Microsoft.AspNetCore.Mvc;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Domain.ViewModels.SuggestedCase;

namespace TestWithValue.Web.Controllers
{
    public class SuggestedCaseController : Controller
    {
        private readonly ISuggestedCaseService _suggestedCaseService;

        public SuggestedCaseController(ISuggestedCaseService suggestedCaseService)
        {
            _suggestedCaseService = suggestedCaseService;
        }

        // نمایش لیست پیشنهادات
        public async Task<IActionResult> Index()
        {
            // دریافت تمام پرونده‌ها از سرویس
            var suggestedCases = await _suggestedCaseService.GetSuggestedCasesAsync();

            // دریافت تاریخ امروز
            var today = DateOnly.FromDateTime(DateTime.Today); // تبدیل تاریخ امروز به DateOnly

            // فیلتر کردن پرونده‌ها بر اساس تاریخ امروز و اینکه IsAccepted برابر false باشد
            var filteredCases = suggestedCases
                .Where(x => !x.IsAccepted && x.Date == today) // فیلتر بر اساس تاریخ روز
                .ToList();

            // تبدیل به ViewModel
            var model = filteredCases.Select(x => new SuggestedCaseViewModel
            {
                SuggestedCaseId = x.SuggestedCaseId,
                Title = x.Title,
                CaseType = x.CaseType,
                LocationName = x.LocationName,
                Date = x.Date // تاریخ به صورت DateOnly نگهداری می‌شود
            }).ToList();

            return View(model);
        }

        // تایید یک پیشنهاد
        [HttpPost]
        [Route("SuggestedCase/AcceptCase/{suggestedCaseId}")]
        public async Task<IActionResult> AcceptCase(int suggestedCaseId)
        {
            // فراخوانی متد سرویس برای تغییر وضعیت پرونده
            await _suggestedCaseService.AcceptSuggestedCaseAsync(suggestedCaseId);

            // ریدایرکت به صفحه ای که لیست پرونده‌ها را نمایش می‌دهد
            return RedirectToAction("Index");
        }
    }
}
