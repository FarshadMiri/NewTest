using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        public int UserId { get; set; }  // کلید خارجی به کاربر

        public int TopicId { get; set; }  // کلید خارجی به موضوع

        [ForeignKey("TopicId")]
        public virtual Tbl_Topic Topic { get; set; }  // رابطه به جدول موضوعات

        public DateTime AddedDate { get; set; } = DateTime.Now;  // تاریخ افزودن به سبد
    }
}
