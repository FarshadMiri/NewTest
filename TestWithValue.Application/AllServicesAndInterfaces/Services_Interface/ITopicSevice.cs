using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Topic;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface ITopicSevice
    {
        Task<IEnumerable<ShowTopicViewModel>> GetTopicByTestId(int testId, int totalScore);

    }
}
