using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.OperationResult
{
    public enum OperationStatus
    {
        Success = 10,
        NotFound = 404,
        InvalidInput = 400,
        ServerError = 500,
        Unauthorized = 401,
        // سایر وضعیت‌ها
    }

}
