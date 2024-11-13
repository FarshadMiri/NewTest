using Microsoft.AspNetCore.Mvc;
using TestWithValue.Domain.OperationResult;

namespace TestWithValue.Web.ViewComponents.OperationResultViewComponent
{
    public class OperationResultViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // دریافت OperationResult از TempData
            var operationResult = TempData["OperationResult"] as OperationResult;

            if (operationResult == null)
            {
                // اگر نتیجه عملیات موجود نبود، چیزی نمایش داده نمی‌شود
                return Content(string.Empty);
            }

            string message;

            // مدیریت پیام‌ها بر اساس کد وضعیت
            switch (operationResult.Status)
            {
                case OperationStatus.Success:
                    message = "عملیات با موفقیت انجام شد.";
                    break;
                case OperationStatus.NotFound:
                    message = "موردی پیدا نشد.";
                    break;
                case OperationStatus.InvalidInput:
                    message = "ورودی نامعتبر است.";
                    break;
                case OperationStatus.ServerError:
                    message = $"خطای سرور: {operationResult.Message}";
                    break;
                case OperationStatus.Unauthorized:
                    message = "دسترسی غیرمجاز.";
                    break;
                default:
                    message = "وضعیت ناشناخته.";
                    break;
            }

            // ارسال پیام به ویو
            return View("/Views/Shared/Components/OperatonResult/OperationResult.cshtml", message);
        }
    }
}
