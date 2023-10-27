﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProcurement.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApprovingDocs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovingDocumentId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ApprovingDocumentId",
                table: "Users",
                column: "ApprovingDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Documents_ApprovingDocumentId",
                table: "Users",
                column: "ApprovingDocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Documents_ApprovingDocumentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ApprovingDocumentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ApprovingDocumentId",
                table: "Users");
        }
    }
}
