using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Contract;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task AddContractAsync(Tbl_Contract contract)
        {
            await _contractRepository.AddContractAsync(contract);
        }

        public async Task<Tbl_Contract> GetContractByCaseIdAsync(int caseId)
        {
            return await _contractRepository.GetContractByCaseIdAsync(caseId);
        }
        public async Task UpdateContractAsync(Tbl_Contract contract)
        {
            await _contractRepository.UpdateContractAsync(contract);
        }

        public async Task<IEnumerable<ContractViewModel>> GetContractsByUserIdAsync(string userId)
        {
            // داده‌ها را از ریپازیتوری دریافت کنید
            var contracts = await _contractRepository.GetContractsByUserIdAsync(userId);

            // تبدیل داده‌ها به ViewModel
            var contractViewModels = contracts.Select(c => new ContractViewModel
            {
                ContractId = c.ContractId,
                ContractTitle = c.ContractTitle,
                FullName = c.FullName,
                Status = c.Status.ToString(), // تبدیل وضعیت به متن
                ContractDate = c.ContractDate.ToString("yyyy/MM/dd") // فرمت تاریخ
            });

            return contractViewModels;
        }


    }

}
