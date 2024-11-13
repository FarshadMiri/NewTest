using Microsoft.AspNetCore.Mvc;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Domain.ViewModels.Question;
using TestWithValue.Domain.ViewModels.Result;

//namespace TestWithValue.Web.ViewComponents
//{
//    public class ShowQuestionsViewComponent : ViewComponent
//    {
//        private readonly IQuestionService _questionService;
//        private readonly IOptionService _optionService;

//        public ShowQuestionsViewComponent(IQuestionService questionService, IOptionService optionService)
//        {
//            _questionService = questionService;
//            _optionService = optionService;
//        }

//        public async Task<IViewComponentResult> InvokeAsync(int index, int testId)
//        {
//            var questions = await _questionService.GetQuestionsByTestId(testId);
//            var questionsList = questions.ToList();

//            if (index < 0 || index >= questionsList.Count)
//            {
//                return Content("Invalid question index.");
//            }

//            var currentQuestion = questionsList[index];
//            var options = await _optionService.GetOptionByQuestionIdAndTestId(currentQuestion.QuestionId, testId);

//            var viewModel = new ShowQuestionsWithSelectedOptionViewModel
//            {
//                QuestionId = currentQuestion.QuestionId,
//                QuestionText = currentQuestion.QuestionText,
//                CurrentQuestionIndex = index,
//                Options = options,
//                IsLastQuestion = index == questionsList.Count - 1,
//                TestId = testId
//            };

//            ViewBag.TotalQuestions = questionsList.Count;

//            return View("/Views/Shared/Components/Question/ShowQuestions.cshtml", viewModel);
//        }
//    }
//}
