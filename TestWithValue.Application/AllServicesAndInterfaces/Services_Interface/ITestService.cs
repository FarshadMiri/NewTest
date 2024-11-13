using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Test;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public  interface ITestService
    {
        Task<IEnumerable<ShowTestViewModel>> GetAllTests();
        Task<ShowTestViewModel> GetTestById(int testId);

    }
}
