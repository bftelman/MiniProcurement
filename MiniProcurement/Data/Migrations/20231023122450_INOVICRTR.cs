using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProcurement.Data.Migrations
{
    /// <inheritdoc />
    public partial class INOVICRTR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "InvoiceRequestItems",
                newName: "PurchaseRequestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRequestItems_PurchaseRequestItemId",
                table: "InvoiceRequestItems",
                column: "PurchaseRequestItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceRequestItems_PurchaseRequestItems_PurchaseRequestItemId",
                table: "InvoiceRequestItems",
                column: "PurchaseRequestItemId",
                principalTable: "PurchaseRequestItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceRequestItems_PurchaseRequestItems_PurchaseRequestItemId",
                table: "InvoiceRequestItems");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceRequestItems_PurchaseRequestItemId",
                table: "InvoiceRequestItems");

            migrationBuilder.RenameColumn(
                name: "PurchaseRequestItemId",
                table: "InvoiceRequestItems",
                newName: "ItemId");
        }
    }
}
