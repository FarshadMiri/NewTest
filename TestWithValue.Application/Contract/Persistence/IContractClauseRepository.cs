using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface IContractClauseRepository
    {
        Task<IEnumerable<Tbl_ContractClause>> GetAllContractClausesAsync();
        Task<Tbl_ContractClause> GetContractClauseByIdAsync(int clauseId);
    }
}
