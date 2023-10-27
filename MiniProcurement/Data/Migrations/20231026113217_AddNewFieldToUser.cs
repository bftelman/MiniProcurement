using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProcurement.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasApprovedDoc",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentStatus",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasApprovedDoc",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DocumentStatus",
                table: "Documents");
        }
    }
}
