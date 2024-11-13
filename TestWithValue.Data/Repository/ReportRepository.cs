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
    public class ReportRepository : IReportRepository
    {
        private readonly TestWithValueDbContext _context;

        public ReportRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        public async Task SaveReportAsync(Tbl_ReportInfo reportInfo)
        {
            try
            {
                await _context.tbl_ReportInfos.AddAsync(reportInfo);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var inner = ex.InnerException.Message;
                inner.Trim();

            }
        }

        public async Task<IEnumerable<Tbl_ReportInfo>> GetReportsByUserIdAsync(string userId)
        {
            return await _context.tbl_ReportInfos.Where(r => r.UserId == userId).ToListAsync();
        }
    }
}
