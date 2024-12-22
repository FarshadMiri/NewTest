using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_Contract
    {
        [Key]
        public int ContractId { get; set; }

        // کلید خارجی به Tbl_Case
        [ForeignKey("Case")]
        public int CaseId { get; set; }
        public Tbl_Case Case { get; set; }

        public string ContractTitle { get; set; }
        public string FullName { get; set; }
        public DateTime ContractDate { get; set; }

        public ContractStatus Status { get; set; } // وضعیت قرارداد
        public bool UserConfirmed { get; set; } // آیا کاربر قرارداد را تایید کرده است؟

        // کلید خارجی به IdentityUser
        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }

}
