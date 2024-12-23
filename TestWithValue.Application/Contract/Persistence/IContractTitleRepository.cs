using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface IContractTitleRepository
    {
        Task<IEnumerable<Tbl_ContractTitle>> GetAllContractTitlesAsync();
        Task<Tbl_ContractTitle> GetContractTitleByIdAsync(int titleId);
    }
}
