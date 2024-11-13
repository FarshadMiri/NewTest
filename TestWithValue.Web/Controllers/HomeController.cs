using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface.ActionMessage_s_Interface;
using TestWithValue.Domain.ViewModels.Ticket;
using TestWithValue.Web.Models;

namespace TestWithValue.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IActionMessageService _actionMessageService;
        private readonly ITicketService _ticketService;
        public HomeController(ILogger<HomeController> logger, IActionMessageService actionMessageService, ITicketService ticketService)
        {
            _logger = logger;
            _actionMessageService = actionMessageService;
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // دریافت UserId از Identity
            var tickets = await _ticketService.GetAllTicketsByUserIdAsync(userId); // دریافت لیست تیکت‌ها

            return View(tickets); // ارسال لیست تیکت‌ها به ویو
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       

    }
}
