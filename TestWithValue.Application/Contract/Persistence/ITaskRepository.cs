using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tbl_Task>> GetTasksByDateAsync(DateOnly date);
        Task AddTaskAsync(Tbl_Task task);
        Task UpdateTaskAsync(Tbl_Task task);
        Task<IEnumerable<Tbl_Task>> GetTasksByUserIdAsync(string userId);
        Task<Tbl_Task> GetTaskByIdAsync(int taskId);
        Task SaveMessageAsync(Tbl_TaskMessage taskMessage); // برای ذخیره پیام
        Task UpdateMessageAsync(Tbl_TaskMessage taskMessage);
        Task<IEnumerable<Tbl_TaskMessage>> GetMessagesByTicketIdAsync(int taskId);
        Task<Tbl_Task> GetOpenTicketForUserByTitleAsync(string userId, string title);
        Task<IEnumerable<Tbl_Task>> GetAllTasksAsync();
    }
}
