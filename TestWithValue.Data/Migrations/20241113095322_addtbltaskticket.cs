using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWithValue.Data.Migrations
{
    /// <inheritdoc />
    public partial class addtbltaskticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Tickets_tbl_TicketStatus_TicketStatusId",
                        column: x => x.TicketStatusId,
                        principalTable: "tbl_TicketStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TaskDateString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_tbl_Tasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_Tasks_tbl_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "tbl_Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_TicketMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TicketMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_TicketMessages_tbl_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "tbl_Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Tasks_TicketId",
                table: "tbl_Tasks",
                column: "TicketId",
                unique: true,
                filter: "[TicketId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Tasks_UserId",
                table: "tbl_Tasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TicketMessages_TicketId",
                table: "tbl_TicketMessages",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Tickets_TicketStatusId",
                table: "tbl_Tickets",
                column: "TicketStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Tasks");

            migrationBuilder.DropTable(
                name: "tbl_TicketMessages");

            migrationBuilder.DropTable(
                name: "tbl_Tickets");
        }
    }
}
