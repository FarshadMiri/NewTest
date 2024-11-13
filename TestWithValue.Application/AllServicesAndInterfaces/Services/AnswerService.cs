using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Answer;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        public AnswerService(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;

        }
        public async Task<AnswerViewModel> GetAnswerByQuestionId(int questionId)
        {
            var answer=await _answerRepository.GetAnswerByQuestionId(questionId);
            var answeVM=_mapper.Map<AnswerViewModel>(answer);
            return answeVM;
        }

        public async Task<IEnumerable<AnswerViewModel>> GetAnswerByTestIdAndUserId(int testId, int UserId)
        {
            var answers=await _answerRepository.GetAnswerByTestIdAndUserId(testId, UserId);
            var answerVM=_mapper.Map<IEnumerable<AnswerViewModel>>(answers);    
            return answerVM;
        }

        public async Task SaveAnswer(AnswerViewModel answer)
        {
            var answers = new Tbl_Answer()
            {

                 QuestionId=answer.QuestionId,
                  OptionId=answer.OptionId,
                   AnswerValue=answer.AnswerValue,
                TestId = answer.TestId,
                UserId =1
            };
            await _answerRepository.SaveAnswer(answers);


        }

        public async Task UpdateAnswer(AnswerViewModel answer)
        {
            var answers = new Tbl_Answer()
            {
                AnswerId = answer.AnswerId,
                QuestionId = answer.QuestionId,
                UserId = 1,
                 OptionId=answer.OptionId,
                 AnswerValue = answer.AnswerValue,
                TestId = answer.TestId,
            };
            await _answerRepository.UpdateAnswer(answers);
        }
    }
}
