using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Task;
using TestWithValue.Domain.ViewModels.Ticket;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface ITaskService
    {
        Task<IEnumerable<Tbl_Task>> GetTasksByDateAsync(DateOnly date);
        Task AddTaskAsync(Tbl_Task task);
        Task MarkTaskAsDoneAsync(int taskId);
        Task<IEnumerable<TaskViewModel>> GetTasksByUserIdAsync(string userId);
        Task<TaskViewModel> GetTaskByIdAsync(int taskId);
        Task UpdateTaskStatusAsync(int taskId, bool isDone);
        Task SaveMessageAsync(int taskId, string senderId, string message); // ذخیره پیام
        Task UpdateMessageAsync(int taskId, string newMessage);
        Task<IEnumerable<TaskMessageViewModel>> GetMessagesByTicketIdAsync(int taskId);
        Task<TaskViewModel> GetOpenTaskForUserByTitleAsync(string userId, string title);
        Task<IEnumerable<TaskViewModel>> GetAllTasksAsync();



    }
}
