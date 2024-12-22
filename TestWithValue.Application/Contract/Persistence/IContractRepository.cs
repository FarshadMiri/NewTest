using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface IContractRepository
    {
        Task AddContractAsync(Tbl_Contract contract);
        Task<Tbl_Contract> GetContractByCaseIdAsync(int caseId);
        Task UpdateContractAsync(Tbl_Contract contract);
        Task<IEnumerable<Tbl_Contract>> GetContractsByUserIdAsync(string userId);
    }

}
