
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Mvc;
using Stimulsoft.System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Domain.ViewModels.Report;
using TestWithValue.Domain.ViewModels.UserInfo;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace TestWithValue.Web.Controllers
{
	
	public class UserController : Controller
	{
		private readonly IUserSevice _userSevice;
		private readonly IUserInfoService _userInfoService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IReportService _reportService; 

        public UserController(IUserSevice userSevice,IUserInfoService userInfoService, UserManager<IdentityUser> userManager,IReportService reportService)
		{
			StiLicense.LoadFromString("6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OJN40hxJjK5JbrxU+NrJ3E0OUAve6MDSIxK3504G4vSTqZezogz9ehm+xS8zUyh3tFhCWSvIoPFEEuqZTyO744uk+ezyGDj7C5jJQQjndNuSYeM+UdsAZVREEuyNFHLm7gD9OuR2dWjf8ldIO6Goh3h52+uMZxbUNal/0uomgpx5NklQZwVfjTBOg0xKBLJqZTDKbdtUrnFeTZLQXPhrQA5D+hCvqsj+DE0n6uAvCB2kNOvqlDealr9mE3y978bJuoq1l4UNE3EzDk+UqlPo8KwL1XM+o1oxqZAZWsRmNv4Rr2EXqg/RNUQId47/4JO0ymIF5V4UMeQcPXs9DicCBJO2qz1Y+MIpmMDbSETtJWksDF5ns6+B0R7BsNPX+rw8nvVtKI1OTJ2GmcYBeRkIyCB7f8VefTSOkq5ZeZkI8loPcLsR4fC4TXjJu2loGgy4avJVXk32bt4FFp9ikWocI9OQ7CakMKyAF6Zx7dJF1nZw");
			_userSevice = userSevice;
			_userInfoService = userInfoService;
			_userManager = userManager;	

		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult ShowPrint()
		{
			return View("ShowPrint");
		}

        public async Task<IActionResult> UserInfo()
        {
            var user = await _userManager.GetUserAsync(User);

            // استفاده از await برای دریافت نتیجه نهایی
            var userinfo = await _userInfoService.GetUserInfoAsync(user.Id);

            // اگر اطلاعاتی پیدا نشود
            if (userinfo == null)
            {
                return NotFound();
            }

            // ارسال مدل به ویو
            return View(userinfo);
        }

        public async Task<IActionResult> Print()
		{
			StiReport report = new StiReport();

			try
			{
				// بارگذاری فایل MRT از مسیر صحیح
				report.Load(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Report/Report.mrt"));

				// دریافت داده‌های کاربران
				var users = await _userSevice.GetAllUsersAsync();

				// ثبت داده‌ها در گزارش
				report.RegData("dt", users);
			}
			catch (Exception ex)
			{
				// مدیریت خطاها
				var inner = ex.InnerException?.Message ?? ex.Message;
				inner.TrimEnd();
				return BadRequest(inner);
			}

			// بازگرداندن نتیجه گزارش
			return StiNetCoreViewer.GetReportResult(this, report);
        }

        public async Task<IActionResult> ShowRequest()
        {
            var user = await _userManager.GetUserAsync(User);
            var userinfo = await _userInfoService.GetUserInfoAsync(user.Id);

            if (userinfo == null)
            {
                return RedirectToAction("CompleteUserInfo", "Auth");
            }

            StiReport report = new StiReport();

            try
            {
                // بارگذاری گزارش
                report.Load(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Report/Report2.mrt"));

                // ثبت داده‌ها
                report.RegData("dt", userinfo);
                report.Dictionary.Synchronize();

                // رندر کردن گزارش
                report.Render();

                // تنظیمات PDF
                StiPdfExportSettings pdfSettings = new StiPdfExportSettings();

                string reportFileName = $"Report_{user.Id}_{DateTime.Now:MM}.pdf";
                string reportFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedReports", reportFileName);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // صادرات گزارش به PDF
                    report.ExportDocument(StiExportFormat.Pdf, memoryStream, pdfSettings);

                    // ذخیره بایت‌ها به یک فایل در مسیر مشخص‌شده
                    System.IO.File.WriteAllBytes(reportFilePath, memoryStream.ToArray());
                }

                // تایید موفقیت‌آمیز بودن ذخیره‌سازی
                if (!System.IO.File.Exists(reportFilePath))
                {
                    throw new FileNotFoundException("Exported PDF not found.");
                }
                var reportInfoVM = new ReportViewModel()
                {
                    FileName = reportFileName,
                    FilePath = reportFilePath,
                    UserId = user.Id,

                };
                //await _reportService.SaveReportInfoAsync(reportInfoVM);
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(inner);
            }

            return StiNetCoreViewer.GetReportResult(this, report);
        }


        public IActionResult ViewEvent()
		{
			return StiNetCoreViewer.ViewerEventResult(this);
		}

        public async Task<IActionResult> RequestEdit()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var reportFileName = $"Report_{user.Id}_{DateTime.Now:MM}.pdf";
            string reportFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedReports", reportFileName);

            if (!System.IO.File.Exists(reportFilePath))
            {
                return Json(new { success = false, message = "فایل PDF یافت نشد." });
            }

            // استخراج متن از فایل PDF
            string message = ExtractTextFromPdf(reportFilePath);

            // فراخوانی متد SendMessageToAgent در هاب
            var title = "درخواست ویرایش PDF";
            return Json(new { success = true, message = message });
        }

        public string ExtractTextFromPdf(string path)
        {
            StringBuilder text = new StringBuilder();

            using (PdfReader pdfReader = new PdfReader(path))
            using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
            {
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    // استخراج متن از هر صفحه
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i), strategy);

                    // تبدیل متن به UTF-8 و انجام بازسازی RTL
                    string utf8Text = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pageText)));

                    // بازسازی جهت راست به چپ
                    utf8Text = ReverseRtlText(utf8Text);

                    text.Append(utf8Text);
                }
            }

            return text.ToString();
        }

        // متد کمکی برای بازسازی جهت راست به چپ
        private string ReverseRtlText(string input)
        {
            // حذف فاصله‌های اضافی و معکوس کردن کاراکترها در خطوط
            string[] lines = input.Split('\n');
            StringBuilder result = new StringBuilder();

            foreach (var line in lines)
            {
                // معکوس کردن متن هر خط برای ترتیب صحیح راست به چپ
                var reversedLine = Regex.Replace(line.Trim(), @"\s+", " ");
                result.AppendLine(new string(reversedLine.Reverse().ToArray()));
            }

            return result.ToString();
        }

    }
}
