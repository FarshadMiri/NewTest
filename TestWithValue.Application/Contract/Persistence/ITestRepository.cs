using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface ITestRepository
    {
        Task<IEnumerable<Tbl_Test>> GetAllTests();
        Task<Tbl_Test> GetTestById(int testId);
    }
}
