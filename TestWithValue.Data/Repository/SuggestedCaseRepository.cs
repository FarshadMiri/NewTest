using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Data.Repository
{
    public class SuggestedCaseRepository : ISuggestedCaseRepository
    {
        private readonly TestWithValueDbContext _context;

        public SuggestedCaseRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        public void AddSuggestedCases(List<Tbl_SuggestedCase> suggestedCases)
        {
            _context.tbl_SuggestedCases.AddRange(suggestedCases);
            _context.SaveChanges();
        }
    }
}
