using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Contract
{
    public class ContractViewModel
    {
        public int ContractId { get; set; }
        public string ContractTitle { get; set; }
        public string FullName { get; set; } // نام کاربر
        public string Status { get; set; } // وضعیت قرارداد به صورت متن
        public string ContractDate { get; set; } // تاریخ قرارداد به فرمت مناسب
    }
}
