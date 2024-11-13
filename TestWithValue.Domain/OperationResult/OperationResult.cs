using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.OperationResult
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public OperationStatus Status { get; set; }
        public object Data { get; set; }

        public OperationResult(bool success, string message, OperationStatus status, object data = null)
        {
            Success = success;
            Message = message;
            Status = status;
            Data = data;
        }
    }

}
