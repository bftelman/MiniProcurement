using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProcurement.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDepartmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId1",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManagerUserId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId1",
                table: "Users",
                column: "DepartmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId1",
                table: "Users",
                column: "DepartmentId1",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerUserId",
                table: "Departments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
