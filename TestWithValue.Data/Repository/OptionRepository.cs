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
    public class OptionRepository : IOptionRepository
    {
        private readonly TestWithValueDbContext _context;
        public OptionRepository(TestWithValueDbContext context)
        {
             _context = context;
                
        }
        public async Task<IEnumerable<Tbl_Option>> GetOptionByQuestionIdAndTestId(int questionId,int testId)
        {
            var options = await _context.tbl_Options
                                .Where(o =>  o.QuestionId == questionId && o.TestId==testId)
                                .ToListAsync();

            return options;

        }
    }
}
