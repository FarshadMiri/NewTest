using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_ContractTitle
    {
        [Key]
        public int TitleId { get; set; }           // شناسه عنوان قرارداد
        public string TitleName { get; set; }      // نام عنوان قرارداد

        // ارتباط با قراردادها:
        public virtual ICollection<Tbl_PartyContract> Contracts { get; set; }
    }
}
