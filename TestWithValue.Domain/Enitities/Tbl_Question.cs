using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestWithValue.Domain.Enitities.Tbl_Test;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_Question
    {
        [Key]
        public int QuestionId { get; set; }  // کلید اصلی
        public string QuestionText { get; set; }  // متن سوال
       
        [ForeignKey("Test")]
        public int TestId { get; set; }  // کلید خارجی به جدول آزمون‌ها

        // ناوبری به آزمون مرتبط
        public Tbl_Test Test { get; set; }

        // ناوبری به گزینه‌های مرتبط
        public ICollection<Tbl_Option> Options { get; set; }
        public Tbl_Answer Answer { get; set; }

    }
}
