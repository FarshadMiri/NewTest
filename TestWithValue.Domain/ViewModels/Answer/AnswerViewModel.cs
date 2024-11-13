using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Answer
{
    public class AnswerViewModel
    {
        public int AnswerId {  get; set; }
        public int  UserId { get; set; }
        public int TestId { get; set; }  // شناسه آزمون
        public int QuestionId { get; set; }  // شناسه سوال
        public int OptionId { get; set; }  // شناسه گزینه انتخاب‌شده
        public int AnswerValue { get; set; }  // ارزش گزینه انتخاب‌شد
    }
}
