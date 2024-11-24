using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Task
{
    public class TaskMessageViewModel
    {
        public string SenderId { get; set; } // شناسه ارسال کننده
        public string Message { get; set; } // متن پیام
        public DateTime SentAt { get; set; } // زمان ارسال
    }
}
