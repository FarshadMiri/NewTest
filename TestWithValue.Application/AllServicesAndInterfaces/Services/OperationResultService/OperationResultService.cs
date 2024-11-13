using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface.OperationResult__s_Interface;
using TestWithValue.Domain.OperationResult;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services.OperationResultService
{
    public class OperationResultService:IOperationResultService
    {
        public OperationResult OperationResultMethod()
        {
            try
            {
                // عملیات مورد نظر
                var resultData = "Some data"; // تعریف داده مورد نظر
                return new OperationResult(true, "عملیات با موفقیت انجام شد.", OperationStatus.Success, resultData);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case KeyNotFoundException _:
                        return new OperationResult(false, "موردی پیدا نشد.", OperationStatus.NotFound);
                    case ArgumentException _:
                        return new OperationResult(false, "ورودی نامعتبر است.", OperationStatus.InvalidInput);
                    default:
                        return new OperationResult(false, $"خطا: {ex.Message}", OperationStatus.ServerError);
                }
            }
        }

    }
}
