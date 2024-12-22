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
    public class ContractRepository : IContractRepository
    {
        private readonly TestWithValueDbContext _context;

        public ContractRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        public async Task AddContractAsync(Tbl_Contract contract)
        {
            _context.tbl_Contracts.Add(contract);
            await _context.SaveChangesAsync();
        }

        public async Task<Tbl_Contract> GetContractByCaseIdAsync(int caseId)
        {
            return await _context.tbl_Contracts
                .FirstOrDefaultAsync(c => c.CaseId == caseId);
        }

        public async Task UpdateContractAsync(Tbl_Contract contract)
        {
            try
            {
                _context.tbl_Contracts.Update(contract); // به‌روزرسانی شیء قرارداد
                await _context.SaveChangesAsync(); // ذخیره تغییرات در دیتابیس

            }
            catch (Exception ex)
            {
                var inner = ex.InnerException.Message;
                inner.Trim();
            }
        }

        public async Task<IEnumerable<Tbl_Contract>> GetContractsByUserIdAsync(string userId)
        {
            return await _context.tbl_Contracts
                                 .Where(c => c.UserId == userId) // فیلتر قراردادها براساس userId
                                 .ToListAsync();
        }


    }
}
