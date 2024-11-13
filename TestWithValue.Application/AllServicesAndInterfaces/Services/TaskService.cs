using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

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

    }

}
