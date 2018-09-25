using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.Migrations
{
    public partial class invoiceRelationToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Invoices_InvoiceId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Invoices_InvoiceId",
                table: "Items",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Invoices_InvoiceId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Invoices_InvoiceId",
                table: "Items",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
