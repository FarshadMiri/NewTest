using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Contract;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface IPartyContractService
    {
        Task<ContractCreateViewModel> GetContractCreateViewModelAsync();
        Task CreateContractAsync(ContractCreateViewModel model);
        Task<Tbl_PartyContract> GetContractByIdAsync(int contractId);
        Task UpdateContractAsync(Tbl_PartyContract contract);
        Task DeleteContractAsync(int contractId);
    }
}
