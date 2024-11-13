using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.ViewModels.Option;

namespace TestWithValue.Domain.ViewModels.Question
{
    public class ShowQuestionsWithSelectedOptionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }  // متن سوال
        public int TestId { get; set; }

        public IEnumerable<ShowOptionViewModel> Options { get; set; }
        public bool IsLastQuestion { get; set; }
        public int CurrentQuestionIndex { get; set; }
        public int? SelectedOptionId {  get; set; }

    }
}
