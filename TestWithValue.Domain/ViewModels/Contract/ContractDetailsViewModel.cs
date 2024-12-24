using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Contract
{
    public class ContractDetailsViewModel
    {
        public int ContractId { get; set; }
        public string Title { get; set; }
        public string PartyOneName { get; set; }
        public string PartyTwoName { get; set; }
        public DateTime ContractDate { get; set; }
        public List<ContractClauseViewModel> Clauses { get; set; }
        public string UserStatus { get; set; } // وضعیت خاص کاربر جاری در قرارداد
    }

    public class ContractClauseViewModel
    {
        public string ClauseText { get; set; }
    }
}
