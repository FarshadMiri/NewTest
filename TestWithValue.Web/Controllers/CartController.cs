using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface.OperationResult__s_Interface;
using TestWithValue.Domain.OperationResult;
using TestWithValue.Domain.ViewModels.CartItem;
using TestWithValue.Domain.ViewModels.Topic;

namespace TestWithValue.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartItemService _cartItemService;
        private readonly IMapper _mapper;
        private readonly IOperationResultService _operationResultService;


       public CartController(ICartItemService cartItemService,IMapper mapper, IOperationResultService operationResultService)
        {
            _cartItemService = cartItemService;
            _mapper = mapper;
            _operationResultService = operationResultService;
                
        }
        public IActionResult Index()
        {
            return View();
        }
        // افزودن به سبد خرید
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddRequestVM addRequest)
        {
            try
            {
                var userId = 1; // در واقع این باید از لاگین کاربر گرفته شود
                var cartItemVM = new CartItemViewModel()
                {
                    TopicId = addRequest.TopicId,
                    UserId = userId
                };

                await _cartItemService.AddToCart(cartItemVM);

                //var operationResultMessage = _operationResultService.OperationResultMethod().Message;
                //TempData["OperationResult"] = JsonConvert.SerializeObject(_operationResultService.OperationResultMethod());
                TempData["OperationResult"] = JsonConvert.SerializeObject(_operationResultService.OperationResultMethod());


                return Ok(); // به جای RedirectToAction
            }
            catch (Exception ex)
            {
                // ثبت خطا و ارسال پیغام خطا به کلاینت
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            await _cartItemService.RemoveFromCart(cartItemId);

            //var operationResultMessage = _operationResultService.OperationResultMethod().Message;
            //TempData["OperationResult"] = JsonConvert.SerializeObject(_operationResultService.OperationResultMethod());

            TempData["OperationResult"] = JsonConvert.SerializeObject(_operationResultService.OperationResultMethod());


            return RedirectToAction("ShowCart");
        }
        [HttpGet]
        public async Task<IActionResult> ShowCart()
        {
            var operationResultJson = TempData["OperationResult"] as string;
            if (!string.IsNullOrEmpty(operationResultJson))
            {
                var operationResult = JsonConvert.DeserializeObject<OperationResult>(operationResultJson);
                ViewBag.OperationResult = operationResult;
            }
            var userId = 1; // در واقع این باید از لاگین کاربر گرفته شود
            var cartItems = await _cartItemService.GetCartItemsByUserId(userId);
            var cartItemVMs = cartItems.Select(item => new CartItemViewModel
            {
                CartItemId = item.CartItemId,
                     TopicId=item.TopicId ,
                TopicName = item.Topic.TopicName,

                
            }).ToList();
            return View(cartItemVMs);
        }


    }
}
