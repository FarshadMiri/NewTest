using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Task
{
    public class AddTaskDto
    {
        public string TaskDate { get; set; } // تاریخ به صورت رشته
        public string Title { get; set; } // عنوان وظیفه
    }
}
