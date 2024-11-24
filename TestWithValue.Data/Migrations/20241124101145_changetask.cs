using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWithValue.Data.Migrations
{
    /// <inheritdoc />
    public partial class changetask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Tasks_tbl_Tickets_TicketId",
                table: "tbl_Tasks");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Tasks_TicketId",
                table: "tbl_Tasks");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "tbl_Tasks");

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "tbl_Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Tickets_TaskId",
                table: "tbl_Tickets",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Tickets_tbl_Tasks_TaskId",
                table: "tbl_Tickets",
                column: "TaskId",
                principalTable: "tbl_Tasks",
                principalColumn: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Tickets_tbl_Tasks_TaskId",
                table: "tbl_Tickets");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Tickets_TaskId",
                table: "tbl_Tickets");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "tbl_Tickets");

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "tbl_Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Tasks_TicketId",
                table: "tbl_Tasks",
                column: "TicketId",
                unique: true,
                filter: "[TicketId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Tasks_tbl_Tickets_TicketId",
                table: "tbl_Tasks",
                column: "TicketId",
                principalTable: "tbl_Tickets",
                principalColumn: "Id");
        }
    }
}
