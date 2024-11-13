using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface IAnswerRepository
    {
        Task<bool> SaveAnswer(Tbl_Answer answer);
        Task<Tbl_Answer > GetAnswerByQuestionId(int questionId);
        Task<IEnumerable<Tbl_Answer>> GetAnswerByTestIdAndUserId(int testId, int UserId);
        Task<bool> UpdateAnswer(Tbl_Answer answer);
        
    }
}
