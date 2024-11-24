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
    public class Tbl_Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public string Title { get; set; } // عنوان وظیفه

        public DateOnly TaskDate { get; set; }

        public bool IsDone { get; set; } // نشان‌دهنده وضعیت انجام وظیفه

        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; } // ارجاع به IdentityUser
        public string TaskDateString { get; set; } // تاریخ به صورت رشته

    }
}
