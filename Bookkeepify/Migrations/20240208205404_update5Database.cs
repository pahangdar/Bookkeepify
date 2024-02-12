using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookkeepify.Migrations
{
    public partial class update5Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "TransactionDetails",
                newName: "DebitAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "CreditAmount",
                table: "TransactionDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditAmount",
                table: "TransactionDetails");

            migrationBuilder.RenameColumn(
                name: "DebitAmount",
                table: "TransactionDetails",
                newName: "Amount");
        }
    }
}
