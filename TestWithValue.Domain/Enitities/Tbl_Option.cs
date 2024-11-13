using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_Option
    {
        [Key]
        public int OptionId { get; set; }  // کلید اصلی
        public string OptionText { get; set; }  // متن گزینه
        public int Value { get; set; }  // ارزش گزینه
        [ForeignKey("Question")]
        public int QuestionId { get; set; }  // کلید خارجی به جدول سوالات
        public int TestId {  get; set; }
        public Tbl_Test Test { get; set; }

        // ناوبری به سوال مرتبط
        public Tbl_Question Question { get; set; }
         
    }
}
