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
        public async Task<List<Tbl_SuggestedCase>> GetSuggestedCasesAsync()
        {
            return await _context.tbl_SuggestedCases.ToListAsync();
        }

        public async Task AcceptSuggestedCaseAsync(int suggestedCaseId)
        {
            var suggestedCase = await _context.tbl_SuggestedCases
                .FirstOrDefaultAsync(x => x.SuggestedCaseId == suggestedCaseId);

            if (suggestedCase != null)
            {
                // تغییر وضعیت یا انجام عملیاتی به عنوان "قبول کردن"
                // مثلا تغییر یک فیلد یا ثبت در وضعیت جدید
                // SuggestedCase.Accepted = true; // این یک مثال است
                _context.SaveChanges();
            }
        }
    }
}
