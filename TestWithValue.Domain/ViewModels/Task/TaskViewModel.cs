using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Task
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; } // عنوان وظیفه
        public DateOnly TaskDate { get; set; } // تاریخ وظیفه
        public bool IsDone { get; set; } // وضعیت انجام وظیفه
    }
}
