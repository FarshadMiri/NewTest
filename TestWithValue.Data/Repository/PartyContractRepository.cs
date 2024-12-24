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
    public class PartyContractRepository : IPartyContractRepository
    {
        private readonly TestWithValueDbContext _context;

        public PartyContractRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        // دریافت قرارداد با شناسه خاص
        public async Task<Tbl_PartyContract> GetContractByIdAsync(int contractId)
        {
            return await _context.tbl_PartyContracts
                                 .Include(c => c.ContractClauseMappings) // اگر نیاز به شامل شدن بندهای قرارداد دارید
                                 .FirstOrDefaultAsync(c => c.ContractId == contractId);
        }

        // دریافت همه قراردادها
        public async Task<IEnumerable<Tbl_PartyContract>> GetAllContractsAsync()
        {
            return await _context.tbl_PartyContracts.ToListAsync();
        }

        // اضافه کردن قرارداد جدید
        public async Task AddContractAsync(Tbl_PartyContract contract)
        {
            _context.tbl_PartyContracts.Add(contract);
            await _context.SaveChangesAsync();
        }

        // بروزرسانی قرارداد
        public async Task UpdateContractAsync(Tbl_PartyContract contract)
        {
            _context.tbl_PartyContracts.Update(contract);
            await _context.SaveChangesAsync();
        }

        // حذف قرارداد
        public async Task DeleteContractAsync(int contractId)
        {
            var contract = await GetContractByIdAsync(contractId);
            if (contract != null)
            {
                _context.tbl_PartyContracts.Remove(contract);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Tbl_PartyContract> GetContractWithClausesAsync(int contractId)
        {
            return await _context.tbl_PartyContracts
                                 .Include(c => c.ContractClauseMappings)
                                     .ThenInclude(mapping => mapping.Clause) // بارگذاری بندهای قرارداد
                                 .FirstOrDefaultAsync(c => c.ContractId == contractId);
        }
        public async Task<IEnumerable<Tbl_PartyContract>> GetContractsForUserAsync(string userId)
        {
            return await _context.tbl_PartyContracts
                .Where(c => c.PartyOneId == userId || c.PartyTwoId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Tbl_PartyContract>> GetContractsByStatusAsync(string status)
        {
            return await _context.tbl_PartyContracts
                .Where(c => c.Status == status)
                .ToListAsync();
        }



    }
}
