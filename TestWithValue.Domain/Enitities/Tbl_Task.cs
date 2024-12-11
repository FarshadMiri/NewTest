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

        public TimeOnly? TaskStartTime { get; set; } // ساعت شروع (اختیاری)

        public TimeOnly? TaskEndTime { get; set; } // ساعت پایان (اختیاری)

        public bool IsDone { get; set; } // نشان‌دهنده وضعیت انجام وظیفه

        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; } // ارجاع به IdentityUser

        [ForeignKey("Location")]
        public int? LocationId { get; set; } // شناسه موقعیت (اختیاری)

        public Tbl_Location Location { get; set; } // ارجاع به Tbl_Location

        public string? LocationName { get; set; } // نام موقعیت (اضافه شده)

        public string TaskDateString { get; set; } // تاریخ به صورت رشته
        public ICollection<Tbl_TaskMessage> Messages { get; set; } = new List<Tbl_TaskMessage>();
    }
}
