using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IdentityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_User_CreatedById1",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_CreatedById1",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "Organizations");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId1",
                table: "User",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_OrganizationId1",
                table: "User",
                column: "OrganizationId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Organizations_OrganizationId1",
                table: "User",
                column: "OrganizationId1",
                principalTable: "Organizations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Organizations_OrganizationId1",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_OrganizationId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                table: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Organizations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById1",
                table: "Organizations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_CreatedById1",
                table: "Organizations",
                column: "CreatedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_User_CreatedById1",
                table: "Organizations",
                column: "CreatedById1",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
