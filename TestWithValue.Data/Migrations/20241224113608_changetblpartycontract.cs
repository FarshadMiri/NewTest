using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWithValue.Data.Migrations
{
    /// <inheritdoc />
    public partial class changetblpartycontract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleName",
                table: "tbl_PartyContracts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleName",
                table: "tbl_PartyContracts");
        }
    }
}
