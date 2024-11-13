using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface.ActionMessage_s_Interface;
using TestWithValue.Domain.ViewModels.Answer;
using TestWithValue.Domain.ViewModels.Option;
using TestWithValue.Domain.ViewModels.Question;
using TestWithValue.Domain.ViewModels.Result;

namespace TestWithValue.Web.Controllers
{
    public class QuizController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IQuestionService _questionService;
        private readonly ITestService _testService;
        private readonly IOptionService _optiontService;
        private readonly IAnswerService _answerService;
        private readonly ITopicSevice _topicService;
        private readonly IActionMessageService _actionMessageService;


        public QuizController(IQuestionService questionService, IOptionService optionService, ITestService testService, IAnswerService answerService,ITopicSevice topicSevice, IMapper mapper,IActionMessageService actionMessageService)
        {
            _mapper = mapper;
            _questionService = questionService;
            _testService = testService;
            _optiontService = optionService;
            _answerService = answerService; 
            _topicService = topicSevice;
            _actionMessageService = actionMessageService;   
        }

        public async Task<IActionResult> Start()
        {
            _actionMessageService.AddMessage("به صفحه آزمون خوش امدید", "success");

            var tests = await _testService.GetAllTests();
            return View(tests);
        }

        // دریافت سوالات
        [HttpGet]
        public async Task<IActionResult> ShowQuestions(int index, int testId)
        {
            var questions = await _questionService.GetQuestionsByTestId(testId);
            var questionsList = questions.ToList();

            // اطمینان از اینکه ایندکس معتبر است
            if (index < 0 || index >= questionsList.Count)
            {
                return NotFound();
            }

            // یافتن سوال فعلی بر اساس ایندکس
            var currentQuestion = questionsList[index];

            // دریافت گزینه‌ها برای سوال فعلی
            var options = await _optiontService.GetOptionByQuestionIdAndTestId(currentQuestion.QuestionId, testId);

            var viewModel = new ShowQuestionsWithSelectedOptionViewModel
            {
                QuestionId = currentQuestion.QuestionId,
                QuestionText = currentQuestion.QuestionText,
                CurrentQuestionIndex = index,
                Options = options,
                IsLastQuestion = index == questionsList.Count - 1,
                TestId = testId
            };
            ViewBag.TotalQuestions = questionsList.Count;

            return View(viewModel);


            //return View();
        }




        [HttpPost]
        public async Task<IActionResult> SaveAnswer([FromBody] AnswerViewModel model)
        {
            

            if (ModelState.IsValid)
            {
                model.UserId = 1; // دریافت UserId کاربر از context یا session


                var existingAnswer = await _answerService.GetAnswerByQuestionId(model.QuestionId);

                if (existingAnswer != null)
                {
                   // اگر پاسخی از قبل وجود دارد، آن را آپدیت کنید
                  existingAnswer.AnswerValue = model.AnswerValue;
                  existingAnswer.OptionId = model.OptionId;

                   await _answerService.UpdateAnswer(existingAnswer);
                }
                else
                {
                    // اگر پاسخی وجود ندارد، پاسخ جدید ایجاد کنید
                    await _answerService.SaveAnswer(model);
                }

                return Ok();
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAnswer(int questionId, int testId)
        {
            // استفاده از سرویس برای دریافت پاسخ بر اساس questionId و testId
            var answer = await _answerService.GetAnswerByQuestionId(questionId);

            // بررسی اینکه آیا پاسخی پیدا شده است یا خیر
            if (answer == null)
            {
                return NotFound(); // یا می‌توانید مقدار پیش‌فرضی برگردانید
            }

            // برگرداندن پاسخ به صورت JSON
            return Json(answer); // یا return Ok(answer); نیز می‌تواند استفاده شود
        }
        // پایان آزمون و نمایش نتیجه
        public async Task<IActionResult> ShowResult(int testId)
        {
            var userId = 1;
            var answers = await _answerService.GetAnswerByTestIdAndUserId(testId, userId);
            if (answers != null)
            {
                // محاسبه مجموع امتیازات
                int totalScore = answers.Sum(a => a.AnswerValue);
                var topics = await _topicService.GetTopicByTestId(testId, totalScore);
                var topicsVM = new ShowResultViewModel
                {
                    Topics = topics
                };
                return View(topicsVM);
            }

            return BadRequest();

        }

        // هدایت پس از اتمام زمان آزمون
        public IActionResult Timeout()
        {
            return RedirectToAction("Start");
        }
    }
}
