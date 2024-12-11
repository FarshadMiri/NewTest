using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_SuggestedCase
    {
        [Key]
        public int SuggestedCaseId { get; set; } // کلید اصلی
        public int CaseId { get; set; } // ارجاع به پرونده اصلی
        public string Title { get; set; }
        public string CaseType { get; set; }
        public string LocationName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string CreatedBy { get; set; } // برای ثبت شخص ایجاد کننده (اختیاری)
        public DateTime CreatedAt { get; set; } // تاریخ ایجاد رکورد
    }

}
