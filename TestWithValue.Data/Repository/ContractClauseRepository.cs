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
    public class ContractClauseRepository : IContractClauseRepository
    {
        private readonly TestWithValueDbContext _context;

        public ContractClauseRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tbl_ContractClause>> GetAllContractClausesAsync()
        {
            return await _context.tbl_ContractClauses.ToListAsync();
        }

        public async Task<Tbl_ContractClause> GetContractClauseByIdAsync(int clauseId)
        {
            return await _context.tbl_ContractClauses
                                 .FirstOrDefaultAsync(c => c.ClauseId == clauseId);
        }
    }
}
