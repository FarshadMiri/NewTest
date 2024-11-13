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
    public class Tbl_Request
    {
        [Key]
        public int RequestId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; } // تغییر نوع به string برای IdentityUser
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } // مثلاً "جدید" یا "در حال بررسی"

        public IdentityUser User { get; set; } // ارجاع به IdentityUser
    }
}
