using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Task
{
    public class AddTaskDto
    {
        public string TaskDate { get; set; }
        public string Title { get; set; } // عنوان وظیفه
        public string TaskStartTime { get; set; } // ساعت شروع (اختیاری)
        public string TaskEndTime { get; set; } // ساعت پایان (اختیاری)
    }
}
