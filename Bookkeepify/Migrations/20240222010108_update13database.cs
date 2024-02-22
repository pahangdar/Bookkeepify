using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookkeepify.Migrations
{
    public partial class update13database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoiceDetails_Invoices_InvoiceId",
                table: "invoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_invoiceDetails_Products_ProductId",
                table: "invoiceDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_invoiceDetails",
                table: "invoiceDetails");

            migrationBuilder.RenameTable(
                name: "invoiceDetails",
                newName: "InvoiceDetails");

            migrationBuilder.RenameIndex(
                name: "IX_invoiceDetails_ProductId",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_invoiceDetails_InvoiceId",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Products_ProductId",
                table: "InvoiceDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceId",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Products_ProductId",
                table: "InvoiceDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails");

            migrationBuilder.RenameTable(
                name: "InvoiceDetails",
                newName: "invoiceDetails");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_ProductId",
                table: "invoiceDetails",
                newName: "IX_invoiceDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "invoiceDetails",
                newName: "IX_invoiceDetails_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_invoiceDetails",
                table: "invoiceDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_invoiceDetails_Invoices_InvoiceId",
                table: "invoiceDetails",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_invoiceDetails_Products_ProductId",
                table: "invoiceDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
