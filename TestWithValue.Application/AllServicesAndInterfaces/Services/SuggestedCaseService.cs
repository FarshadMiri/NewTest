using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class SuggestedCaseService : ISuggestedCaseService
    {
        private readonly ISuggestedCaseRepository _suggestedCaseRepository;
        public SuggestedCaseService(ISuggestedCaseRepository suggestedCaseRepository)
        {
            _suggestedCaseRepository = suggestedCaseRepository;      
        }
        public async Task<List<Tbl_SuggestedCase>> GetSuggestedCasesAsync()
        {
            return await _suggestedCaseRepository.GetSuggestedCasesAsync();
        }

        public async Task AcceptSuggestedCaseAsync(int suggestedCaseId)
        {
            await _suggestedCaseRepository.AcceptSuggestedCaseAsync(suggestedCaseId);
        }
    }
}
