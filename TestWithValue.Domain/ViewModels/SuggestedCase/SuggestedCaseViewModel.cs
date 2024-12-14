using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.SuggestedCase
{
    public class SuggestedCaseViewModel
    {
        public int SuggestedCaseId { get; set; }
        public string Title { get; set; }
        public string CaseType { get; set; }
        public string LocationName { get; set; }
        public DateOnly Date { get; set; }
        public string FormattedDate => Date.ToString("yyyy/MM/dd"); // فرمت تاریخ برای نمایش
    }
}
