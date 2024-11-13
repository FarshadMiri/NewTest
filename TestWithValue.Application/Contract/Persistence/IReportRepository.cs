using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface IReportRepository
    {
        Task SaveReportAsync(Tbl_ReportInfo reportInfo);
        Task<IEnumerable<Tbl_ReportInfo>> GetReportsByUserIdAsync(string userId);
    }
}
