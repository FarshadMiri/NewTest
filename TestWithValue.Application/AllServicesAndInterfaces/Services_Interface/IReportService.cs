using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.ViewModels.Report;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface IReportService
    {
        Task SaveReportInfoAsync(ReportViewModel reportViewModel);
    }
}
