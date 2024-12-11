using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestWithValue.Domain.Enitities
{
    public class Tbl_Case
    {
        [Key]
        public int CaseId { get; set; }
        public string Title { get; set; }
        public string CaseType { get; set; }
        public bool IsDone { get; set; }

        // کلید خارجی به Tbl_Location
        [ForeignKey("Location")]
        public int? LocationId { get; set; }
        public Tbl_Location Location { get; set; } // پراپرتی ناویگیشن به Tbl_Location
        public string LocationName { get; set; } // فیلد برای ذخیره نام موقعیت

        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; } // ارجاع به IdentityUser
    }
}
