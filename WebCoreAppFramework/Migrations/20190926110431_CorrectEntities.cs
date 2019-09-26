using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCoreAppFramework.Migrations
{
    public partial class CorrectEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserDetailsViewModelId",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserDetailsViewModelId",
                table: "AspNetUserClaims",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserDetailsViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetailsViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserDetailsViewModelId",
                table: "AspNetUserRoles",
                column: "UserDetailsViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserDetailsViewModelId",
                table: "AspNetUserClaims",
                column: "UserDetailsViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_UserDetailsViewModel_UserDetailsViewModelId",
                table: "AspNetUserClaims",
                column: "UserDetailsViewModelId",
                principalTable: "UserDetailsViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_UserDetailsViewModel_UserDetailsViewModelId",
                table: "AspNetUserRoles",
                column: "UserDetailsViewModelId",
                principalTable: "UserDetailsViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_UserDetailsViewModel_UserDetailsViewModelId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_UserDetailsViewModel_UserDetailsViewModelId",
                table: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "UserDetailsViewModel");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserDetailsViewModelId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_UserDetailsViewModelId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "UserDetailsViewModelId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UserDetailsViewModelId",
                table: "AspNetUserClaims");
        }
    }
}
