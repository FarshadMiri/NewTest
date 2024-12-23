using Microsoft.AspNetCore.Mvc;
using TestWithValue.Domain.ViewModels.Contract;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;

namespace TestWithValue.Controllers
{
    public class ContractController : Controller
    {
        private readonly IPartyContractService _partyContractService;

        public ContractController(IPartyContractService partyContractService)
        {
            _partyContractService = partyContractService;
        }

        // نمایش فرم ایجاد قرارداد
        public async Task<IActionResult> Create()
        {
            var model = await _partyContractService.GetContractCreateViewModelAsync();
            return View(model);
        }

        // پردازش ایجاد قرارداد
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContractCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _partyContractService.CreateContractAsync(model);
                return RedirectToAction(nameof(Index)); // هدایت به صفحه لیست قراردادها
            }
            else
            {
                var updatedModel = await _partyContractService.GetContractCreateViewModelAsync();
                return View(updatedModel);
            }
        }

        // صفحه نمایش لیست قراردادها (در صورت نیاز)
        public IActionResult Index()
        {
            // می‌توانید لیست قراردادها را اینجا نمایش دهید
            return View();
        }
    }
}
