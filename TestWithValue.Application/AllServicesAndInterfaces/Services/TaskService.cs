using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Task;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
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
                 TicketId=t.TicketId,
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
    }

}
