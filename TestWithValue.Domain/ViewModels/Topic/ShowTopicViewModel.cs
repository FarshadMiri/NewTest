using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Topic
{
    public class ShowTopicViewModel
    {
        public int TopicId { get; set; }  // کلید اصلی
        public string TopicName { get; set; }  // نام موضوع
        public int Value { get; set; }  // ارزش موضوع
        public int TestId { get; set; }  // کلید خارجی


    }
}
