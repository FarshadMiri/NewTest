using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface ITaskService
    {
        Task<IEnumerable<Tbl_Task>> GetTasksByDateAsync(DateOnly date);
        Task AddTaskAsync(Tbl_Task task);
        Task MarkTaskAsDoneAsync(int taskId);
    }
}
