using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Question;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface IQuestionService
    {
        Task<IEnumerable<ShowQuestionViewModel>> GetQuestionsByTestId(int testId);

    }
}
