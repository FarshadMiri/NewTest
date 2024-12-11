using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.CaseViewModel;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _repository;
        private readonly ILocationService _locationService;

        public CaseService(ICaseRepository repository, ILocationService locationService)
        {
            _repository = repository;
            _locationService = locationService;
        }

        public async Task<IEnumerable<CaseViewModel>> GetAllCasesAsync()
        {
            var cases = await _repository.GetAllCasesAsync();
            var locations = await _locationService.GetLocationsForDropdownAsync();  // گرفتن لوکیشن‌ها از سرویس

            return cases.Select(c => new CaseViewModel
            {
                CaseId = c.CaseId,
                Title = c.Title,
                CaseType = c.CaseType,
                IsDone = c.IsDone,
                Date = c.Date.ToString("yyyy-MM-dd"),
                Time = c.Time.ToString("HH:mm"),
            });
        }

        public async Task<CaseViewModel> GetCaseByIdAsync(int caseId)
        {
            var caseEntity = await _repository.GetCaseByIdAsync(caseId);
            if (caseEntity == null) return null;

            var locations = await _locationService.GetLocationsForDropdownAsync();  // گرفتن لوکیشن‌ها از سرویس

            return new CaseViewModel
            {
                CaseId = caseEntity.CaseId,
                Title = caseEntity.Title,
                CaseType = caseEntity.CaseType,
                IsDone = caseEntity.IsDone,
                Date = caseEntity.Date.ToString("yyyy-MM-dd"),
                Time = caseEntity.Time.ToString("HH:mm"),
            };
        }

        public async Task<IEnumerable<CaseViewModel>> GetCasesByUserIdAsync(string userId)
        {
            var cases = await _repository.GetCasesByUserIdAsync(userId);
            var locations = await _locationService.GetLocationsForDropdownAsync();  // گرفتن لوکیشن‌ها از سرویس

            return cases.Select(c => new CaseViewModel
            {
                CaseId = c.CaseId,
                Title = c.Title,
                CaseType = c.CaseType,
                IsDone = c.IsDone,
                Date = c.Date.ToString("yyyy-MM-dd"),
                Time = c.Time.ToString("HH:mm"),
                Location=c.LocationName
            });
        }

        public async Task AddCaseAsync(Tbl_Case newCase)
        {
            await _repository.AddCaseAsync(newCase);
            await _repository.SaveChangesAsync();
        }
    }
}
