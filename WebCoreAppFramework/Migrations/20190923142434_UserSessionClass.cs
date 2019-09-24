using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCoreAppFramework.Migrations
{
    public partial class UserSessionClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentSessionId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CurrentUserSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<long>(nullable: true),
                    GeoLocationId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentUserSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentUserSession_GeoLocations_GeoLocationId",
                        column: x => x.GeoLocationId,
                        principalTable: "GeoLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentUserSession_ApplicationTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "ApplicationTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrentSessionId",
                table: "AspNetUsers",
                column: "CurrentSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentUserSession_GeoLocationId",
                table: "CurrentUserSession",
                column: "GeoLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentUserSession_TenantId",
                table: "CurrentUserSession",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CurrentUserSession_CurrentSessionId",
                table: "AspNetUsers",
                column: "CurrentSessionId",
                principalTable: "CurrentUserSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CurrentUserSession_CurrentSessionId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CurrentUserSession");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrentSessionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentSessionId",
                table: "AspNetUsers");
        }
    }
}
