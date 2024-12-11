using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWithValue.Data.Migrations
{
    /// <inheritdoc />
    public partial class change2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "tbl_Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "tbl_Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Tasks_LocationId",
                table: "tbl_Tasks",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Tasks_tbl_Locations_LocationId",
                table: "tbl_Tasks",
                column: "LocationId",
                principalTable: "tbl_Locations",
                principalColumn: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Tasks_tbl_Locations_LocationId",
                table: "tbl_Tasks");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Tasks_LocationId",
                table: "tbl_Tasks");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "tbl_Tasks");

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "tbl_Tasks");
        }
    }
}
