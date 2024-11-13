using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.ViewModels.Topic;

namespace TestWithValue.Domain.ViewModels.Result
{
    public class ShowResultViewModel
    {
        public IEnumerable<ShowTopicViewModel> Topics { get; set; }
    }
}
