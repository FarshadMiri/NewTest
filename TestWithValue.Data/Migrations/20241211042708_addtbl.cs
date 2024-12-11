using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestWithValue.Data.Migrations
{
    /// <inheritdoc />
    public partial class addtbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Cases",
                columns: table => new
                {
                    CaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Cases", x => x.CaseId);
                    table.ForeignKey(
                        name: "FK_tbl_Cases_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_Cases_tbl_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "tbl_Locations",
                        principalColumn: "LocationId");
                });

            migrationBuilder.InsertData(
                table: "tbl_Locations",
                columns: new[] { "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, "شعبه دادگاه تهران" },
                    { 2, "شعبه دادگاه مشهد" },
                    { 3, "شعبه دادگاه اصفهان" },
                    { 4, "شعبه دادگاه شیراز" },
                    { 5, "شعبه دادگاه تبریز" },
                    { 6, "شعبه دادگاه کرج" },
                    { 7, "شعبه دادگاه اهواز" },
                    { 8, "شعبه دادگاه قم" },
                    { 9, "شعبه دادگاه رشت" },
                    { 10, "شعبه دادگاه یزد" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Cases_LocationId",
                table: "tbl_Cases",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Cases_UserId",
                table: "tbl_Cases",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Cases");

            migrationBuilder.DropTable(
                name: "tbl_Locations");
        }
    }
}
