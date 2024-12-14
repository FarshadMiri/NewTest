using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Case
{
    public class GetCasesByTaskDto
    {
        public string TaskDate { get; set; } // تاریخ وظیفه به صورت شمسی
        public string LocationName { get; set; } // نام موقعیت مکانی
    }
}
