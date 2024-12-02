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

        //public async Task AddTask(string userId, string title, DateTime taskDate)
        //{
        //    // بررسی اینکه آیا کاربر احراز هویت شده است یا خیر
        //    var currentUserId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    if (string.IsNullOrEmpty(currentUserId))
        //    {
        //        throw new HubException("کاربر احراز هویت نشده است.");
        //    }

        //    // بررسی وظیفه موجود با عنوان مشخص
        //    var existingTask = await _taskService.GetOpenTaskForUserByTitleAsync(userId, title);

        //    if (existingTask != null)
        //    {
        //        if (!existingTask.IsDone)
        //        {
        //            // پیام به کاربر در صورت وجود درخواست در حال بررسی
        //            await Clients.Caller.SendAsync("ShowMessage", "شما یک درخواست در حال بررسی دارید.");
        //            return;
        //        }
        //        else
        //        {
        //            // پیام به کاربر در صورت وجود درخواست قبلی کامل شده
        //            await Clients.Caller.SendAsync("ShowMessage", "شما یک درخواست مشابه دارید که قبلاً بررسی شده است.");
        //            return;
        //        }
        //    }

        //    // ایجاد نمونه‌ای از وظیفه با استفاده از اطلاعات دریافتی
        //    var taskDateOnly = DateOnly.FromDateTime(taskDate);
        //    var task = new Tbl_Task
        //    {
        //        TaskDate = taskDateOnly, // استفاده از DateOnly
        //        Title = title,           // تنظیم عنوان وظیفه
        //        IsDone = false,
        //        UserId = userId          // تنظیم UserId کاربر ایجادکننده وظیفه
        //    };

        //    // افزودن وظیفه به دیتابیس
        //    await _taskService.AddTaskAsync(task);

        //    // ارسال وظیفه جدید به گروه پشتیبان‌ها
        //    await Clients.Group("Agent").SendAsync("ReceiveNewTask", task);
        //}
        public async Task AddTask(string userId, string title, DateTime taskDate)
        {
            // بررسی اینکه آیا کاربر احراز هویت شده است یا خیر
            var currentUserId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new HubException("کاربر احراز هویت نشده است.");
            }

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

            var taskDateOnly = DateOnly.FromDateTime(taskDate);
            // دریافت وظایف پشتیبان برای تاریخ انتخاب شده
            var tasks = await _taskService.GetTasksByDateAsync(taskDateOnly); // فرض بر اینکه این متد وظایف پشتیبان را برای تاریخ مشخص شده می‌آورد.

            // ساعت شروع و پایان کاری پشتیبان
            DateTime startOfDay = taskDate.Date.AddHours(7);  // 7 صبح
            DateTime endOfDay = taskDate.Date.AddHours(19);   // 7 عصر

            // مرتب‌سازی وظایف بر اساس زمان شروع
            var sortedTasks = tasks
                .Where(task => task.TaskStartTime >= TimeOnly.FromDateTime(startOfDay) && task.TaskEndTime <= TimeOnly.FromDateTime(endOfDay))
                .OrderBy(task => task.TaskStartTime)
                .ToList();

            // جستجوی دودویی برای پیدا کردن اولین زمان خالی
            DateTime? availableStartTime = FindFirstAvailableTime(sortedTasks, startOfDay, endOfDay);

            // اگر هیچ ساعت خالی پیدا نشد
            if (!availableStartTime.HasValue)
            {
                await Clients.Caller.SendAsync("ShowMessage", "هیچ زمان خالی برای ثبت وظیفه وجود ندارد.");
                return;
            }

            // تعیین ساعت پایان وظیفه (1 ساعت بعد از زمان شروع)
            DateTime taskEndTime = availableStartTime.Value.AddHours(1);

            // ایجاد وظیفه جدید با زمان شروع و پایان محاسبه شده
            var task = new Tbl_Task
            {
                TaskDate = DateOnly.FromDateTime(taskDate),  // استفاده از DateOnly
                Title = title,                               // عنوان وظیفه
                TaskStartTime = TimeOnly.FromDateTime(availableStartTime.Value), // زمان شروع
                TaskEndTime = TimeOnly.FromDateTime(taskEndTime),  // زمان پایان
                IsDone = false,
                UserId = userId                             // UserId پشتیبان
            };

            // افزودن وظیفه به دیتابیس
            await _taskService.AddTaskAsync(task);

            // ارسال وظیفه جدید به گروه پشتیبان‌ها
            await Clients.Group("Agent").SendAsync("ReceiveNewTask", task);

            // ارسال تاییدیه به کاربر
            await Clients.Caller.SendAsync("ShowMessage", $"وظیفه شما در ساعت {availableStartTime.Value.ToString("HH:mm")} ثبت شد.");
        }

        private DateTime? FindFirstAvailableTime(List<Tbl_Task> sortedTasks, DateTime startOfDay, DateTime endOfDay)
        {
            DateTime startOfDayDateTime = startOfDay;
            DateTime endOfDayDateTime = endOfDay;

            // اگر لیست وظایف خالی است، زمان خالی از شروع روز کاری است
            if (sortedTasks.Count == 0)
            {
                return startOfDayDateTime;
            }

            // بررسی زمان خالی قبل از اولین وظیفه
            var firstTaskStartTime = ConvertTimeOnlyToDateTime(sortedTasks.First().TaskStartTime);
            if (firstTaskStartTime.HasValue && firstTaskStartTime > startOfDayDateTime)
            {
                return startOfDayDateTime;
            }

            // بررسی فواصل بین وظایف
            for (int i = 0; i < sortedTasks.Count - 1; i++)
            {
                var currentTaskEndTime = ConvertTimeOnlyToDateTime(sortedTasks[i].TaskEndTime);
                var nextTaskStartTime = ConvertTimeOnlyToDateTime(sortedTasks[i + 1].TaskStartTime);

                if (currentTaskEndTime.HasValue && nextTaskStartTime.HasValue
                    && nextTaskStartTime > currentTaskEndTime.Value)
                {
                    if (currentTaskEndTime.Value.AddHours(1) <= nextTaskStartTime)
                    {
                        return currentTaskEndTime.Value;
                    }
                }
            }

            // بررسی زمان خالی بعد از آخرین وظیفه
            var lastTaskEndTime = ConvertTimeOnlyToDateTime(sortedTasks.Last().TaskEndTime);
            if (lastTaskEndTime.HasValue && lastTaskEndTime.Value.AddHours(1) <= endOfDayDateTime)
            {
                return lastTaskEndTime.Value;
            }

            return null;
        }

        private DateTime? ConvertTimeOnlyToDateTime(TimeOnly? timeOnly)
        {
            if (timeOnly.HasValue)
            {
                return DateTime.Today.Add(timeOnly.Value.ToTimeSpan());
            }
            return null;
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
