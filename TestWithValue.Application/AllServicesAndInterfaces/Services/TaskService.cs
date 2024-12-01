using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Task;
using TestWithValue.Domain.ViewModels.Ticket;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;   

        public TaskService(ITaskRepository taskRepository,IMapper mapper)
        {
            _taskRepository = taskRepository;
             _mapper = mapper;  
        }

        public async Task<IEnumerable<Tbl_Task>> GetTasksByDateAsync(DateOnly date)
        {
            return await _taskRepository.GetTasksByDateAsync(date);
        }

        public async Task AddTaskAsync(Tbl_Task task)
        {
            
            await _taskRepository.AddTaskAsync(task);
        }

        public Task MarkTaskAsDoneAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskViewModel>> GetTasksByUserIdAsync(string userId)
        {
            var tasks = await _taskRepository.GetTasksByUserIdAsync(userId);

            // تبدیل داده‌های مدل به ViewModel
            return tasks.Select(t => new TaskViewModel
            {
                TaskId = t.TaskId,
                Title = t.Title,
                TaskDate = t.TaskDate,
                IsDone = t.IsDone
            });
        }

        public async Task<TaskViewModel> GetTaskByIdAsync(int taskId)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null) return null;

            return new TaskViewModel
            {
                TaskId = task.TaskId,
                Title = task.Title,
                TaskDate = task.TaskDate,
                IsDone = task.IsDone
            };
        }
        public async Task UpdateTaskStatusAsync(int taskId, bool isDone)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task != null)
            {
                task.IsDone = isDone;
                await _taskRepository.UpdateTaskAsync(task);
            }
        }

        public async Task  SaveMessageAsync(int taskId, string senderId, string message)
        {
            var taskMessage = new Tbl_TaskMessage
            {
                 TaskId = taskId,
                SenderId = senderId,
                Message = message,
                SentAt = DateTime.Now
            };

            await _taskRepository.SaveMessageAsync(taskMessage); // ذخیره پیام
        }

        public async Task UpdateMessageAsync(int taskId, string newMessage)
        {
            // دریافت تمام پیام‌ها بر اساس TicketId
            var messages = await _taskRepository.GetMessagesByTicketIdAsync(taskId);

            if (messages == null || !messages.Any())
            {
                throw new Exception($"No messages found for task ID {taskId}");
            }

            // فرض: فقط اولین پیام را به‌روزرسانی می‌کنیم (یا معیار خاصی را انتخاب کنید)
            var taskMessage = messages.First();
            taskMessage.Message = newMessage;
            taskMessage.SentAt = DateTime.UtcNow;

            // به‌روزرسانی پیام
            await _taskRepository.UpdateMessageAsync(taskMessage);

        }

        public async Task<IEnumerable<TaskMessageViewModel>> GetMessagesByTicketIdAsync(int taskId)
        {
            var messages = await _taskRepository.GetMessagesByTicketIdAsync(taskId);
            return messages.Select(msg => new TaskMessageViewModel
            {
                SenderId = msg.SenderId,
                Message = msg.Message,
                SentAt = msg.SentAt
            });
        }

        public async Task<TaskViewModel> GetOpenTaskForUserByTitleAsync(string userId, string title)
        {
            var task = await _taskRepository.GetOpenTicketForUserByTitleAsync(userId, title);
            var taskVM = _mapper.Map<TaskViewModel>(task);
            return taskVM;
        }

        public async Task<IEnumerable<TaskViewModel>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();

            // تبدیل داده‌ها به ViewModel و پر کردن مقادیر
            return tasks.Select(task => new TaskViewModel
            {
                TaskId = task.TaskId, // شناسه تسک
                Title = task.Title, // عنوان تسک
                TaskDate = task.TaskDate, // تاریخ تسک
                TaskStartTime = task.TaskStartTime, // ساعت شروع
                TaskEndTime = task.TaskEndTime, // ساعت پایان
                IsDone = task.IsDone// وضعیت انجام تسک
            });
        }
    }

}
