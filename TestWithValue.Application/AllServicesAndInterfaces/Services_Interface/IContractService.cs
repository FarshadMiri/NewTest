using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Contract;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface IContractService
    {
        Task AddContractAsync(Tbl_Contract contract);
        Task<Tbl_Contract> GetContractByCaseIdAsync(int caseId);
        Task UpdateContractAsync(Tbl_Contract contract);
        Task<IEnumerable<ContractViewModel>> GetContractsByUserIdAsync(string userId);
    }

}
