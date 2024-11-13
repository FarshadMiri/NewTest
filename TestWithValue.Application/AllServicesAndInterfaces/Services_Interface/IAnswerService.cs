using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Answer;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface IAnswerService
    {
        Task SaveAnswer(AnswerViewModel answer);
        Task<AnswerViewModel> GetAnswerByQuestionId(int questionId);
        Task<IEnumerable<AnswerViewModel>> GetAnswerByTestIdAndUserId(int testId, int UserId);

        Task UpdateAnswer(AnswerViewModel answer);
    }
}
