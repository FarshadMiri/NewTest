using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text.RegularExpressions;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Ticket;

namespace TestWithValue.Web.Hubs.HubTask
{
    public class TaskHub : Hub
    {
        private readonly ITaskService _taskService;
        private readonly ITicketService _ticketService; 

        public TaskHub(ITaskService taskService, ITicketService ticketService)
        {
            _taskService = taskService;
            _ticketService = ticketService; 
        }
        public async Task CheckUserRole()
        {
            // دریافت نقش کاربر
            if (Context.User.IsInRole("Agent"))
            {
                await Clients.Caller.SendAsync("ReceiveRole", "Agent");
            }
            else if (Context.User.IsInRole("User"))
            {
                await Clients.Caller.SendAsync("ReceiveRole", "User");
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveRole", "Unknown");
            }
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // بر اساس نقش، کاربر را به گروه مناسب اضافه کنید
            if (Context.User.IsInRole("Agent"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Agent");
            }
            else if (Context.User.IsInRole("User") && !string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnConnectedAsync();
        }

        public async Task AddTask(string userId, string title, DateTime taskDate)
        {
            // بررسی اینکه آیا کاربر احراز هویت شده است یا خیر
            var currentUserId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new HubException("کاربر احراز هویت نشده است.");
            }

            // بررسی وظیفه موجود با عنوان مشخص
            var existingTask = await _taskService.GetOpenTaskForUserByTitleAsync(userId, title);

            if (existingTask != null)
            {
                if (!existingTask.IsDone)
                {
                    // پیام به کاربر در صورت وجود درخواست در حال بررسی
                    await Clients.Caller.SendAsync("ShowMessage", "شما یک درخواست در حال بررسی دارید.");
                    return;
                }
                else
                {
                    // پیام به کاربر در صورت وجود درخواست قبلی کامل شده
                    await Clients.Caller.SendAsync("ShowMessage", "شما یک درخواست مشابه دارید که قبلاً بررسی شده است.");
                    return;
                }
            }

            // ایجاد نمونه‌ای از وظیفه با استفاده از اطلاعات دریافتی
            var taskDateOnly = DateOnly.FromDateTime(taskDate);
            var task = new Tbl_Task
            {
                TaskDate = taskDateOnly, // استفاده از DateOnly
                Title = title,           // تنظیم عنوان وظیفه
                IsDone = false,
                UserId = userId          // تنظیم UserId کاربر ایجادکننده وظیفه
            };

            // افزودن وظیفه به دیتابیس
            await _taskService.AddTaskAsync(task);

            // ارسال وظیفه جدید به گروه پشتیبان‌ها
            await Clients.Group("Agent").SendAsync("ReceiveNewTask", task);
        }
        public async Task SendRequestToAgent(string userId, string title, string message)
        {
            var existingTask = await _taskService.GetOpenTaskForUserByTitleAsync(userId, title);
            var currentMessage = await _taskService.GetMessagesByTicketIdAsync(existingTask.TaskId);
            
            if (currentMessage.Count()==0)
            {
                int taskId = existingTask.TaskId;
                await _taskService.SaveMessageAsync(taskId, userId, message);
                // ارسال پیام به گروه پشتیبان‌ها
                await Clients.Group("Agent").SendAsync("ReceiveRequestFromUser", userId, message, title);

            }

        }

        public async Task EditUserRequest(int taskId,  string message)
        {
            try
            {
                // به‌روزرسانی پیام در پایگاه داده
                await _taskService.UpdateMessageAsync(Convert.ToInt32(taskId), message);

                // ارسال پیام به کلاینت
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendEditToUser: {ex.Message}");
            }
        }



        public async Task GetUserTasks()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new HubException("کاربر احراز هویت نشده است.");
            }

            // دریافت تسک‌های مربوط به کاربر
            var tasks = await _taskService.GetTasksByUserIdAsync(userId);

            // ارسال لیست تسک‌ها به خود کاربر
            await Clients.Caller.SendAsync("ReceiveUserTasks", tasks);
        }

        //public async Task GetPendingTasksForAgents()
        //{
        //    if (!Context.User.IsInRole("Agent"))
        //    {
        //        throw new HubException("دسترسی غیرمجاز.");
        //    }

        //    // دریافت تمام تسک‌های باز برای پشتیبان‌ها
        //    var tasks = await _taskService.GetAllPendingTasksAsync();

        //    // ارسال لیست تسک‌ها به پشتیبان حاضر
        //    await Clients.Caller.SendAsync("ReceiveAllPendingTasks", tasks);
        //}

        public async Task MarkTaskAsDone(int taskId)
        {
            if (!Context.User.IsInRole("Agent"))
            {
                throw new HubException("دسترسی غیرمجاز.");
            }

            // به‌روزرسانی وضعیت تسک به انجام‌شده
            await _taskService.MarkTaskAsDoneAsync(taskId);

            // اطلاع‌رسانی به پشتیبان‌ها درباره تسک انجام‌شده
            await Clients.Group("Agent").SendAsync("TaskMarkedAsDone", taskId);
        }
    }
}
