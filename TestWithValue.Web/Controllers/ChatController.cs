﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using System.Globalization;
using Microsoft.AspNetCore.SignalR;
using TestWithValue.Web.HubSupport;
using TestWithValue.Domain.Enitities;
namespace TestWithValue.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ITaskService _taskService;
        private readonly IHubContext<SupportHub> _hubContext;
        public ChatController(ITicketService ticketService, ITaskService taskService, IHubContext<SupportHub> hubContext)
        {
            _ticketService = ticketService;
            _taskService = taskService;
            _hubContext = hubContext;
        }
        public IActionResult Index()
        {
            return View(); // بازگشت به ویو index
        }

        [HttpGet]
        public IActionResult CheckUserRole()
        {
            if (User.IsInRole("User"))
            {
                return Json(new { isLoggedIn = true, role = "User" });
            }
            else if (User.IsInRole("Agent"))
            {
                return Json(new { isLoggedIn = true, role = "Agent" });
            }

            return Json(new { isLoggedIn = false });
        }
        public async Task<IActionResult> SupportChat(string? ticketId)
        {
            // بررسی نقش پشتیبان
            if (User.IsInRole("Agent"))
            {
                // اگر ticketId مقدار داشته باشد
               

                // دریافت لیست تمام تیکت‌ها
                var tickets = await _ticketService.GetAllTicketsAsync();
                return View(tickets); // ارسال لیست تیکت‌ها به ویو
            }

            // در صورت عدم نقش صحیح، به صفحه ورود هدایت شود
            return RedirectToAction("Login", "Auth");
        }

        public IActionResult Tasks()
        {
            return View();
        }

        [HttpGet("tasks/gettasks")]
        public async Task<IActionResult> GetTasks(DateOnly date)
        {
            var tasks = await _taskService.GetTasksByDateAsync(date);

            if (tasks == null || !tasks.Any())
            {
                return Json(new { message = "هیچ وظیفه‌ای برای این تاریخ وجود ندارد." });
            }

            var result = tasks.Select(t => new
            {
                taskId = t.TaskId, // اضافه کردن taskId
                title = t.Title,
                isDone = t.IsDone,
                date = t.TaskDate.ToString("yyyy-MM-dd"),
                ticketId = t.TicketId
            });

            return Json(result);
        }

        [HttpPost("tasks/updatetaskstatus")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] TaskStatusUpdateRequest request)
        {
            if (request.TaskId <= 0)
                return BadRequest("Invalid TaskId");

            await _taskService.UpdateTaskStatusAsync(request.TaskId, request.IsDone);
            return Ok(new { message = "Task status updated successfully" });
        }
    }
}