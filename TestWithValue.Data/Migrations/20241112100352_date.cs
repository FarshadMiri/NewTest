using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWithValue.Data.Migrations
{
    /// <inheritdoc />
    public partial class date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "TaskDate",
                table: "tbl_Tasks",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TaskDate",
                table: "tbl_Tasks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
