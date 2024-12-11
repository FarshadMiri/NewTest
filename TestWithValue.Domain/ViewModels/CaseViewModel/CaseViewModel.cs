using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;


namespace TestWithValue.Domain.ViewModels.CaseViewModel
{

    public class CaseViewModel
    {
        public int CaseId { get; set; }
        public string Title { get; set; }
        public string CaseType { get; set; }
        public bool IsDone { get; set; }

        // اضافه کردن LocationId برای ذخیره انتخابی که از دراپ‌داون می‌آید
        public int? LocationId { get; set; } // نوع nullable برای اختیاری بودن

        // فقط اسم لوکیشن برای نمایش در نمای صفحه
        public string Location { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }
        public string UserName { get; set; } // نام کاربر
        public List<DropdownItem> Locations { get; set; } // لیست لوکیشن‌ها برای دراپ‌داون
    }
}
