using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class TaskStatusUpdateRequest
    {
        public int TaskId { get; set; }
        public bool IsDone { get; set; }
    }
}
