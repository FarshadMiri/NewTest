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
            var suggestedCases = await _suggestedCaseService.GetSuggestedCasesAsync();
            var model = suggestedCases.Select(x => new SuggestedCaseViewModel
            {
                SuggestedCaseId = x.SuggestedCaseId,
                Title = x.Title,
                CaseType = x.CaseType,
                LocationName = x.LocationName,
                Date = x.Date
            }).ToList();

            return View(model);
        }

        // تایید یک پیشنهاد
        [HttpPost]
        public async Task<IActionResult> AcceptCase(int suggestedCaseId)
        {
            await _suggestedCaseService.AcceptSuggestedCaseAsync(suggestedCaseId);
            return RedirectToAction("Index");
        }
    }
}
