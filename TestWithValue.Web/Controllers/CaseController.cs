using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.CaseViewModel;

namespace TestWithValue.Web.Controllers
{
    public class CaseController : Controller
    {
        private readonly ICaseService _caseService;
        private readonly ILocationService _locationService;
        private readonly IContractService _contractService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserInfoService _userInfoService;



        public CaseController(ICaseService caseService, ILocationService locationService, UserManager<IdentityUser> userManager, IContractService contractService, IUserInfoService userInfoService)
        {
            _caseService = caseService;
            _locationService = locationService;
            _userManager = userManager;
            _contractService = contractService;
            _userInfoService = userInfoService;
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
                    ModelState.AddModelError("", "Location not found.");
                    return View(model);
                }

                // ایجاد پرونده جدید
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

                // دریافت اطلاعات کاربر از جدول UserInfo
                var userInfo = await _userInfoService.GetUserInfoAsync(userId);
                if (userInfo == null)
                {
                    ModelState.AddModelError("", "User info not found.");
                    return View(model);
                }

                // ایجاد قرارداد استاتیک
                var contract = new Tbl_Contract
                {
                    CaseId = newCase.CaseId,
                    ContractTitle = newCase.Title,
                    FullName = userInfo.FullName, // پر کردن نام کاربر از جدول UserInfo
                    ContractDate = DateTime.Now,
                    Status = ContractStatus.PendingUserApproval, // وضعیت قرارداد: در انتظار تایید کاربر
                    UserId = userId
                };

                await _contractService.AddContractAsync(contract); // ذخیره قرارداد

                // نمایش قرارداد به کاربر
                return RedirectToAction("ViewContract", "Case", new { contractId = contract.ContractId });
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

        // اکشن برای نمایش قرارداد
        [HttpGet]
        public async Task<IActionResult> ViewContract(int contractId)
        {
            var contract = await _contractService.GetContractByCaseIdAsync(contractId);

            if (contract == null)
            {
                return NotFound();
            }

            return View(contract); // ارسال قرارداد به ویو
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmContractByUser(int contractId, bool UserConfirmed)
        {
            var contract = await _contractService.GetContractByCaseIdAsync(contractId);

            if (contract == null)
            {
                return NotFound(); // قرارداد پیدا نشد
            }


            contract.Status = ContractStatus.Approved; // تغییر وضعیت به تایید شده
            contract.UserConfirmed = true;

            await _contractService.UpdateContractAsync(contract); // استفاده از متد به‌روزرسانی


            return RedirectToAction("ViewContract", new { contractId });
        }
        public async Task<IActionResult> UserContracts()
        {
            var userId = _userManager.GetUserId(User);


            var contracts = await _contractService.GetContractsByUserIdAsync(userId);
            return View(contracts);
        }


    }
}
