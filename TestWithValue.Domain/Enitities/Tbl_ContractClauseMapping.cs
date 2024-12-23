using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_ContractClauseMapping
    {
        public int ContractId { get; set; }      // شناسه قرارداد
        public int ClauseId { get; set; }        // شناسه بند قرارداد

        // ارتباط با جداول:
        public virtual Tbl_PartyContract Contract { get; set; }
        public virtual Tbl_ContractClause Clause { get; set; }
    }
}
