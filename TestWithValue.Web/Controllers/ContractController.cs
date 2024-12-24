using Microsoft.AspNetCore.Mvc;
using TestWithValue.Domain.ViewModels.Contract;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using Microsoft.AspNetCore.Identity;

namespace TestWithValue.Controllers
{
    public class ContractController : Controller
    {
        private readonly IPartyContractService _partyContractService;
        private readonly UserManager<IdentityUser> _userManager; // UserManager برای مدیریت کاربران


        public ContractController(IPartyContractService partyContractService, UserManager<IdentityUser> userManager)
        {
            _partyContractService = partyContractService;
            _userManager= userManager;  
        }

        // نمایش فرم ایجاد قرارداد
        public async Task<IActionResult> Create()
        {
            var model = await _partyContractService.GetContractCreateViewModelAsync();

            // بررسی اینکه آیا همه لیست‌ها پر شده‌اند یا نه
            if (model.ContractTitles == null)
                model.ContractTitles = new List<DropdownItem>();  // پر کردن لیست به صورت پیش‌فرض

            if (model.Users == null)
                model.Users = new List<DropdownItem>();  // پر کردن لیست به صورت پیش‌فرض

            if (model.Lawyers == null)
                model.Lawyers = new List<DropdownItem>();  // پر کردن لیست به صورت پیش‌فرض

            if (model.ContractClauses == null)
                model.ContractClauses = new List<DropdownItem>();  // پر کردن لیست به صورت پیش‌فرض

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
        public async Task<IActionResult> Details(int contractId)
        {
            var model = await _partyContractService.GetContractDetailsAsync(contractId);

            if (model == null)
                return NotFound(); // در صورتی که قرارداد پیدا نشد

            return View(model);
        }


        public async Task<IActionResult> Index()
        {
            var contracts = await _partyContractService.GetAllContractsForIndexAsync();

            return View(contracts);
        }
        public async Task<IActionResult> UserContracts()
        {
            var userId = _userManager.GetUserId(User); // دریافت شناسه کاربر جاری
            if (userId == null)
            {
                return Unauthorized(); // اگر کاربر وارد نشده باشد
            }

            var contracts = await _partyContractService.GetContractsForUserAsync(userId);

            return View(contracts);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveOrRejectContract(int contractId, string action)
        {
            var userId = _userManager.GetUserId(User); // دریافت شناسه کاربر جاری

            var result = await _partyContractService.ApproveOrRejectContractAsync(contractId, userId, action);

            if (!result)
                return BadRequest("Unable to process the request.");

            return RedirectToAction(nameof(UserContracts));
        }
        public async Task<IActionResult> UserContractDetails(int contractId)
        {
            var userId = _userManager.GetUserId(User); // دریافت شناسه کاربر جاری

            var model = await _partyContractService.GetContractDetailsForUserAsync(contractId, userId);

            if (model == null)
                return NotFound();

            return View(model);
        }



    }
}
