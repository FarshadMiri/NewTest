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
    public class Tbl_Topic
    {
        [Key]
        public int TopicId { get; set; }  // کلید اصلی
        public string TopicName { get; set; }  // نام موضوع
        public int Value { get; set; }  // ارزش موضوع

        // کلید خارجی به جدول آزمون‌ها
        [ForeignKey("Test")]
        public int TestId { get; set; }  // کلید خارجی
        public Tbl_Test Test { get; set; }  // ناوبری به آزمون مرتبط
    }
}
