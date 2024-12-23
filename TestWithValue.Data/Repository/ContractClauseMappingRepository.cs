using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Data.Repository
{
    public class ContractClauseMappingRepository : IContractClauseMappingRepository
    {
        private readonly TestWithValueDbContext _context;

        public ContractClauseMappingRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tbl_ContractClauseMapping mapping)
        {
            await _context.tbl_ContractClauseMappings.AddAsync(mapping);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
