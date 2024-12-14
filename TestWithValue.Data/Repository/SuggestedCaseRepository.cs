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
            // پیدا کردن پرونده با شناسه suggestedCaseId
            var suggestedCase = await _context.tbl_SuggestedCases
                .FirstOrDefaultAsync(x => x.SuggestedCaseId == suggestedCaseId);

            // اگر پرونده پیدا شد، وضعیت IsAccepted را به true تغییر می‌دهیم
            if (suggestedCase != null)
            {
                suggestedCase.IsAccepted = true; // تغییر وضعیت به قبول شده
                await _context.SaveChangesAsync(); // ذخیره تغییرات در دیتابیس
            }
        }
    }
}
