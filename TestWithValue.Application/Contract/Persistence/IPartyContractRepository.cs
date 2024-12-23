using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface IPartyContractRepository
    {
        Task<Tbl_PartyContract> GetContractByIdAsync(int contractId);
        Task<IEnumerable<Tbl_PartyContract>> GetAllContractsAsync();
        Task AddContractAsync(Tbl_PartyContract contract);
        Task UpdateContractAsync(Tbl_PartyContract contract);
        Task DeleteContractAsync(int contractId);
    }
}
