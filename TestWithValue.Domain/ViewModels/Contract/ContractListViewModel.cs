using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Contract
{
    public class ContractListViewModel
    {
        public int ContractId { get; set; }
        public string Title { get; set; }
        public string PartyOneName { get; set; }
        public string PartyTwoName { get; set; }
        public DateTime ContractDate { get; set; }
        public string Status { get; set; } // وضعیت کلی قرارداد (مانند تایید شده، رد شده)
        public string UserStatus { get; set; } // وضعیت خاص کاربر جاری در قرارداد
    }
}
