using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Topic;

namespace TestWithValue.Domain.ViewModels.CartItem
{
    public class CartItemViewModel
    {
        [Key]
        public int CartItemId { get; set; }

        public int UserId { get; set; }  // کلید خارجی به کاربر

        public int TopicId { get; set; }  // کلید خارجی به موضوع
        public string TopicName { get; set; }
        public  ShowTopicViewModel Topic {  get; set; }
        public DateTime AddedDate { get; set; }
    }
}
