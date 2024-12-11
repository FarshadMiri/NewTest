using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface ICaseRepository
    {
        Task<IEnumerable<Tbl_Case>> GetAllCasesAsync();
        Task<Tbl_Case> GetCaseByIdAsync(int caseId);
        Task<IEnumerable<Tbl_Case>> GetCasesByUserIdAsync(string userId); // اضافه شده
        Task AddCaseAsync(Tbl_Case newCase);
        Task SaveChangesAsync();
    }
}
