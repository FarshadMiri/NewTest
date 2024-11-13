using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Report;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task SaveReportInfoAsync(ReportViewModel reportViewModel)
        {
            // تبدیل ReportViewModel به ReportInfo
            var reportInfo = new Tbl_ReportInfo
            {
                UserId = reportViewModel.UserId,
                FileName = reportViewModel.FileName,
                FilePath = reportViewModel.FilePath
            };

            await _reportRepository.SaveReportAsync(reportInfo);
        }

    }

}
