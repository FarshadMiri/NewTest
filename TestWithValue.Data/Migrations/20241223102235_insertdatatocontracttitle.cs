using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestWithValue.Data.Migrations
{
    /// <inheritdoc />
    public partial class insertdatatocontracttitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tbl_ContractTitles",
                columns: new[] { "TitleId", "TitleName" },
                values: new object[,]
                {
                    { 1, "قرارداد خدمات" },
                    { 2, "قرارداد عدم افشاء (NDA)" },
                    { 3, "قرارداد محرمانگی" },
                    { 4, "قرارداد مشارکت" },
                    { 5, "قرارداد استخدام" },
                    { 6, "قرارداد اجاره" },
                    { 7, "قرارداد فروش" },
                    { 8, "قرارداد وام" },
                    { 9, "قرارداد تسویه" },
                    { 10, "قرارداد مجوز" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbl_ContractTitles",
                keyColumn: "TitleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbl_ContractTitles",
                keyColumn: "TitleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tbl_ContractTitles",
                keyColumn: "TitleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tbl_ContractTitles",
                keyColumn: "TitleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tbl_ContractTitles",
                keyColumn: "TitleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tbl_ContractTitles",
                keyColumn: "TitleId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "tbl_ContractTitles",
                keyColumn: "TitleId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "tbl_ContractTitles",
                keyColumn: "TitleId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "tbl_ContractTitles",
                keyColumn: "TitleId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "tbl_ContractTitles",
                keyColumn: "TitleId",
                keyValue: 10);
        }
    }
}
