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
    public class ContractTitleRepository : IContractTitleRepository
    {
        private readonly TestWithValueDbContext _context;

        public ContractTitleRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tbl_ContractTitle>> GetAllContractTitlesAsync()
        {
            return await _context.tbl_ContractTitles.ToListAsync();
        }

        public async Task<Tbl_ContractTitle> GetContractTitleByIdAsync(int titleId)
        {
            return await _context.tbl_ContractTitles
                                 .FirstOrDefaultAsync(t => t.TitleId == titleId);
        }
    }
}
