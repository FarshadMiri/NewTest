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
    }
}
