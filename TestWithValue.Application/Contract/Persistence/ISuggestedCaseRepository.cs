﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface ISuggestedCaseRepository
    {
        void AddSuggestedCases(List<Tbl_SuggestedCase> suggestedCases);
        Task<List<Tbl_SuggestedCase>> GetSuggestedCasesAsync(); // برای دریافت پیشنهادات
        Task AcceptSuggestedCaseAsync(int suggestedCaseId); // برای تایید و قبول یک پیشنهاد
    }
}
