using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWithValue.Data.Migrations
{
    /// <inheritdoc />
    public partial class changetbltask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "TaskEndTime",
                table: "tbl_Tasks",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TaskStartTime",
                table: "tbl_Tasks",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskEndTime",
                table: "tbl_Tasks");

            migrationBuilder.DropColumn(
                name: "TaskStartTime",
                table: "tbl_Tasks");
        }
    }
}
