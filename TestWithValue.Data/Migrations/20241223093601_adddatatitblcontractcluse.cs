using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestWithValue.Data.Migrations
{
    /// <inheritdoc />
    public partial class adddatatitblcontractcluse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tbl_ContractClauses",
                columns: new[] { "ClauseId", "ClauseText" },
                values: new object[,]
                {
                    { 1, "بند 1: این قرارداد تحت قوانین جمهوری اسلامی ایران تنظیم شده است." },
                    { 2, "بند 2: طرفین موظف به حفظ اطلاعات محرمانه هستند." },
                    { 3, "بند 3: مدت قرارداد از تاریخ امضا به مدت یک سال معتبر است." },
                    { 4, "بند 4: کلیه اختلافات از طریق داوری حل می‌شود." },
                    { 5, "بند 5: تمامی پرداخت‌ها به صورت ریالی انجام خواهد شد." },
                    { 6, "بند 6: در صورت بروز اختلاف، حکمیت به وکیل تعیین شده واگذار می‌شود." },
                    { 7, "بند 7: هر گونه تخلف منجر به فسخ قرارداد خواهد شد." },
                    { 8, "بند 8: طرفین توافق دارند از امکانات سامانه الکترونیکی استفاده کنند." },
                    { 9, "بند 9: حق تغییر در بندهای قرارداد با توافق طرفین امکان‌پذیر است." },
                    { 10, "بند 10: جریمه تخلف معادل 10 درصد ارزش قرارداد خواهد بود." },
                    { 11, "بند 11: قرارداد فقط با امضای هر دو طرف معتبر است." },
                    { 12, "بند 12: خدمات پشتیبانی شامل هزینه جداگانه خواهد بود." },
                    { 13, "بند 13: قرارداد باید در بازه زمانی توافق شده اجرا شود." },
                    { 14, "بند 14: اطلاعات طرفین باید به صورت دقیق در سامانه وارد شود." },
                    { 15, "بند 15: تعهدات مالی باید ظرف مدت 30 روز تسویه شود." },
                    { 16, "بند 16: طرفین متعهد به رعایت الزامات محیط زیستی هستند." },
                    { 17, "بند 17: تغییرات در قرارداد باید مکتوب و امضا شود." },
                    { 18, "بند 18: قرارداد شامل مالیات بر ارزش افزوده می‌باشد." },
                    { 19, "بند 19: در صورت بروز قوه قهریه، قرارداد به تعلیق در می‌آید." },
                    { 20, "بند 20: هر گونه استفاده تجاری بدون مجوز ممنوع است." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "tbl_ContractClauses",
                keyColumn: "ClauseId",
                keyValue: 20);
        }
    }
}
