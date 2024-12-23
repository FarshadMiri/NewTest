using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWithValue.Data.Migrations
{
    /// <inheritdoc />
    public partial class addpartycontract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
       table: "Tbl_ContractClause",
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
            migrationBuilder.CreateTable(
                name: "tbl_ContractClauses",
                columns: table => new
                {
                    ClauseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClauseText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ContractClauses", x => x.ClauseId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ContractTitles",
                columns: table => new
                {
                    TitleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ContractTitles", x => x.TitleId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PartyContracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleId = table.Column<int>(type: "int", nullable: false),
                    PartyOneId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PartyTwoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PartyOneName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyTwoName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyOneStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyTwoStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractTitleTitleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PartyContracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_tbl_PartyContracts_AspNetUsers_PartyOneId",
                        column: x => x.PartyOneId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_PartyContracts_AspNetUsers_PartyTwoId",
                        column: x => x.PartyTwoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_PartyContracts_tbl_ContractTitles_ContractTitleTitleId",
                        column: x => x.ContractTitleTitleId,
                        principalTable: "tbl_ContractTitles",
                        principalColumn: "TitleId");
                });

            migrationBuilder.CreateTable(
                name: "tbl_ContractClauseMappings",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    ClauseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ContractClauseMappings", x => new { x.ContractId, x.ClauseId });
                    table.ForeignKey(
                        name: "FK_tbl_ContractClauseMappings_tbl_ContractClauses_ClauseId",
                        column: x => x.ClauseId,
                        principalTable: "tbl_ContractClauses",
                        principalColumn: "ClauseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ContractClauseMappings_tbl_PartyContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "tbl_PartyContracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ContractClauseMappings_ClauseId",
                table: "tbl_ContractClauseMappings",
                column: "ClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PartyContracts_ContractTitleTitleId",
                table: "tbl_PartyContracts",
                column: "ContractTitleTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PartyContracts_PartyOneId",
                table: "tbl_PartyContracts",
                column: "PartyOneId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PartyContracts_PartyTwoId",
                table: "tbl_PartyContracts",
                column: "PartyTwoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ContractClauseMappings");

            migrationBuilder.DropTable(
                name: "tbl_ContractClauses");

            migrationBuilder.DropTable(
                name: "tbl_PartyContracts");

            migrationBuilder.DropTable(
                name: "tbl_ContractTitles");
        }
    }
}
