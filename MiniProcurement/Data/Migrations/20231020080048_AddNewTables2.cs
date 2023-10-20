using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProcurement.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceRequestItem_InvoiceRequests_InvoiceRequestId",
                table: "InvoiceRequestItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceRequestItem",
                table: "InvoiceRequestItem");

            migrationBuilder.RenameTable(
                name: "InvoiceRequestItem",
                newName: "InvoiceRequestItems");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceRequestItem_InvoiceRequestId",
                table: "InvoiceRequestItems",
                newName: "IX_InvoiceRequestItems_InvoiceRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceRequestItems",
                table: "InvoiceRequestItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceRequestItems_InvoiceRequests_InvoiceRequestId",
                table: "InvoiceRequestItems",
                column: "InvoiceRequestId",
                principalTable: "InvoiceRequests",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceRequestItems_InvoiceRequests_InvoiceRequestId",
                table: "InvoiceRequestItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceRequestItems",
                table: "InvoiceRequestItems");

            migrationBuilder.RenameTable(
                name: "InvoiceRequestItems",
                newName: "InvoiceRequestItem");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceRequestItems_InvoiceRequestId",
                table: "InvoiceRequestItem",
                newName: "IX_InvoiceRequestItem_InvoiceRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceRequestItem",
                table: "InvoiceRequestItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceRequestItem_InvoiceRequests_InvoiceRequestId",
                table: "InvoiceRequestItem",
                column: "InvoiceRequestId",
                principalTable: "InvoiceRequests",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
