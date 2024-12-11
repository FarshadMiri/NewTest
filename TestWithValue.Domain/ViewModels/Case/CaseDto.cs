using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Case
{
    public class CaseDto
    {
        public int CaseId { get; set; }
        public string Title { get; set; }
        public string CaseType { get; set; }
        public string LocationName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
    }

}
