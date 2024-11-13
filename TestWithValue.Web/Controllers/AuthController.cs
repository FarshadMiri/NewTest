using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Domain.ViewModels.Login;
using TestWithValue.Domain.ViewModels.Register;
using TestWithValue.Domain.ViewModels.UserInfo;

public class AuthController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IProvinceService _provinceService;
    private readonly ICityService _cityService;
    private readonly IOrganizationService _organizationService;
    private readonly IUserInfoService _userInfoService; 



    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IProvinceService provinceService, ICityService cityService, IOrganizationService organizationService,IUserInfoService userInfoService )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _provinceService = provinceService;
        _cityService = cityService;
        _organizationService = organizationService;
        _userInfoService = userInfoService; 
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var User = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(User, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (var identityError in result.Errors)
            {
                ModelState.AddModelError("", identityError.Description);

            }


        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["returnUrl"] = returnUrl;
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("Index", "Home");
        }

        ViewData["returnUrl"] = returnUrl;

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var roles = await _userManager.GetRolesAsync(user);
                string role = roles.Count > 0 ? roles[0] : "Guest";

                TempData["isLoggedIn"] = true;
                TempData["role"] = role;

                if (role == "Agent") // اگر نقش Agent بود
                {
                    return RedirectToAction("SupportChat", "Chat"); // به صفحه supportchat هدایت شود
                }
                else if (role == "User") // اگر نقش کاربر عادی بود
                {
                    return RedirectToAction("Index", "Home"); // به صفحه index هدایت شود و چت در آن نمایش داده شود
                }
            }

            if (result.IsLockedOut)
            {
                ViewData["ErrorMessage"] = "به دلیل 5 بار ورود ناموفق اکانت شما به مدت 5 دقیقه قفل شده است.";
                return View(model);
            }

            ModelState.AddModelError("", "رمز عبور یا نام کاربری درست نمی‌باشد");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Auth");
    }

    [HttpGet]
    public IActionResult CheckUserStatus()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new { isLoggedIn = false });
        }

        string role = "Guest";
        if (User.IsInRole("Agent"))
        {
            role = "Agent";
        }
        else if (User.IsInRole("User"))
        {
            role = "User";
        }

        return Json(new { isLoggedIn = true, role = role });
    }

    [HttpGet]
    public async Task<IActionResult> CompleteUserInfo()
    {
        //var provinces = await _provinceService.GetAllProvinces();
        //var Organizations = await _organizationService.GetAllOrganizations();
        var model = new UserInfoViewModel()
        {
            //Provinces = provinces,
            //Organizations = Organizations


        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CompleteUserInfo(UserInfoViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        //var province = await _provinceService.GetProvinceById(model.ProvinceId);
        //var city = await _cityService.GetCityByCityIdAsync(model.CityId);
        //var organization = await _organizationService.GetOrganizationByIdAsync(Convert.ToInt32(model.Organization));

        //model.Organization = organization.Name;
        model.UserId = user.Id;
        //model.Province = province.Name;
        //model.City = city.Name;

        if (ModelState.IsValid)
        {
            await _userInfoService.SaveUserInfoAsync(model);

            // پس از تکمیل اطلاعات، کاربر را به صفحه نمایش گزارش هدایت کن
            return RedirectToAction("UserInfo", "User");
        }

        // اگر مدل معتبر نیست، ویو را مجدداً بازگردانید و خطاها نمایش داده خواهند شد
        //model.Provinces = await _provinceService.GetAllProvinces();
        //model.Organizations = await _organizationService.GetAllOrganizations();

        return View(model);
    }
 //   public JsonResult GetCitiesByProvince(int provinceId)
	//{
	//	var cities = _cityService.GetCityByProvinceId(provinceId)
	//		.Select(c => new { cityId = c.CityId, name = c.Name })
	//		.ToList();
	//	return Json(cities);
	//}
}
