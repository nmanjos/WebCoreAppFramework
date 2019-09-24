using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCoreAppFramework.Migrations
{
    public partial class UserSessionClass1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentUserSession_ApplicationTenants_TenantId",
                table: "CurrentUserSession");

            migrationBuilder.AlterColumn<long>(
                name: "TenantId",
                table: "CurrentUserSession",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentUserSession_ApplicationTenants_TenantId",
                table: "CurrentUserSession",
                column: "TenantId",
                principalTable: "ApplicationTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentUserSession_ApplicationTenants_TenantId",
                table: "CurrentUserSession");

            migrationBuilder.AlterColumn<long>(
                name: "TenantId",
                table: "CurrentUserSession",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentUserSession_ApplicationTenants_TenantId",
                table: "CurrentUserSession",
                column: "TenantId",
                principalTable: "ApplicationTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
