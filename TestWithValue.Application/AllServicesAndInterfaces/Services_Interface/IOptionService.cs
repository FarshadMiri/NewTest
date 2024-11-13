using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Option;
using TestWithValue.Domain.ViewModels.Question;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface IOptionService
    {
        Task<IEnumerable<ShowOptionViewModel>> GetOptionByQuestionIdAndTestId( int questionId, int testId   );

    }
}
