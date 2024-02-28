using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookkeepify.Migrations
{
    public partial class update13database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditAccountId",
                table: "TransactionTypes",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DebtAccountId",
                table: "TransactionTypes",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypes_CreditAccountId",
                table: "TransactionTypes",
                column: "CreditAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypes_DebtAccountId",
                table: "TransactionTypes",
                column: "DebtAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionTypes_Accounts_CreditAccountId",
                table: "TransactionTypes",
                column: "CreditAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionTypes_Accounts_DebtAccountId",
                table: "TransactionTypes",
                column: "DebtAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionTypes_Accounts_CreditAccountId",
                table: "TransactionTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionTypes_Accounts_DebtAccountId",
                table: "TransactionTypes");

            migrationBuilder.DropIndex(
                name: "IX_TransactionTypes_CreditAccountId",
                table: "TransactionTypes");

            migrationBuilder.DropIndex(
                name: "IX_TransactionTypes_DebtAccountId",
                table: "TransactionTypes");

            migrationBuilder.DropColumn(
                name: "CreditAccountId",
                table: "TransactionTypes");

            migrationBuilder.DropColumn(
                name: "DebtAccountId",
                table: "TransactionTypes");
        }
    }
}
