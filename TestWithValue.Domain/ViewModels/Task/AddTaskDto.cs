using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Task
{
    public class AddTaskDto
    {
        public string Title { get; set; }
        public string TaskDate { get; set; }
        public string TaskStartTime { get; set; }
        public string TaskEndTime { get; set; }
        public int? LocationId { get; set; } // شناسه موقعیت مکانی
        public string LocationName { get; set; } // نام موقعیت مکانی (اضافه شده)
    }
}
