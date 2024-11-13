using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Data.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly TestWithValueDbContext _context;
        public TestRepository(TestWithValueDbContext context)
        {
                _context = context;
        }

        public async Task<IEnumerable<Tbl_Test>> GetAllTests()
        {
            return await _context.tbl_Tests.ToListAsync();

        }

        public async Task<Tbl_Test> GetTestById(int testId)
        {
            return await _context.tbl_Tests.FindAsync(testId);
        }
    }
}
