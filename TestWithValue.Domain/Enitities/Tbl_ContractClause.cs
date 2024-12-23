using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_ContractClause
    {
        [Key]
        public int ClauseId { get; set; }         // شناسه بند قرارداد
        public string ClauseText { get; set; }   // متن بند قرارداد

        // ارتباط با جدول واسط:
        public virtual ICollection<Tbl_ContractClauseMapping> ContractClauseMappings { get; set; }
    }
}
