using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface IOptionRepository
    {
        Task<IEnumerable<Tbl_Option>> GetOptionByQuestionIdAndTestId(int questionId,int testId);
    }
}
