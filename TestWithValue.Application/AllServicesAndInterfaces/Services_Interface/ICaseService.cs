using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.CaseViewModel;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface ICaseService
    {
        Task<IEnumerable<CaseViewModel>> GetAllCasesAsync();
        Task<CaseViewModel> GetCaseByIdAsync(int caseId);
        Task<IEnumerable<CaseViewModel>> GetCasesByUserIdAsync(string userId); // اضافه شده
        Task AddCaseAsync(Tbl_Case newCase);
    }
}
