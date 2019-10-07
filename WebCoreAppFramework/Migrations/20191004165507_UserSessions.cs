using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCoreAppFramework.Migrations
{
    public partial class UserSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CurrentUserSession_CurrentSessionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryLanguage_Countries_CountryId",
                table: "CountryLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryLanguage_Languages_LanguageId",
                table: "CountryLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentUserSession_GeoLocations_GeoLocationId",
                table: "CurrentUserSession");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentUserSession_ApplicationTenants_TenantId",
                table: "CurrentUserSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentUserSession",
                table: "CurrentUserSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryLanguage",
                table: "CountryLanguage");

            migrationBuilder.RenameTable(
                name: "CurrentUserSession",
                newName: "UserSessions");

            migrationBuilder.RenameTable(
                name: "CountryLanguage",
                newName: "CountryLanguages");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentUserSession_TenantId",
                table: "UserSessions",
                newName: "IX_UserSessions_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentUserSession_GeoLocationId",
                table: "UserSessions",
                newName: "IX_UserSessions_GeoLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryLanguage_LanguageId",
                table: "CountryLanguages",
                newName: "IX_CountryLanguages_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSessions",
                table: "UserSessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryLanguages",
                table: "CountryLanguages",
                columns: new[] { "CountryId", "LanguageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserSessions_CurrentSessionId",
                table: "AspNetUsers",
                column: "CurrentSessionId",
                principalTable: "UserSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryLanguages_Countries_CountryId",
                table: "CountryLanguages",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryLanguages_Languages_LanguageId",
                table: "CountryLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_GeoLocations_GeoLocationId",
                table: "UserSessions",
                column: "GeoLocationId",
                principalTable: "GeoLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_ApplicationTenants_TenantId",
                table: "UserSessions",
                column: "TenantId",
                principalTable: "ApplicationTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserSessions_CurrentSessionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryLanguages_Countries_CountryId",
                table: "CountryLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryLanguages_Languages_LanguageId",
                table: "CountryLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_GeoLocations_GeoLocationId",
                table: "UserSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_ApplicationTenants_TenantId",
                table: "UserSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSessions",
                table: "UserSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryLanguages",
                table: "CountryLanguages");

            migrationBuilder.RenameTable(
                name: "UserSessions",
                newName: "CurrentUserSession");

            migrationBuilder.RenameTable(
                name: "CountryLanguages",
                newName: "CountryLanguage");

            migrationBuilder.RenameIndex(
                name: "IX_UserSessions_TenantId",
                table: "CurrentUserSession",
                newName: "IX_CurrentUserSession_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSessions_GeoLocationId",
                table: "CurrentUserSession",
                newName: "IX_CurrentUserSession_GeoLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryLanguages_LanguageId",
                table: "CountryLanguage",
                newName: "IX_CountryLanguage_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentUserSession",
                table: "CurrentUserSession",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryLanguage",
                table: "CountryLanguage",
                columns: new[] { "CountryId", "LanguageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CurrentUserSession_CurrentSessionId",
                table: "AspNetUsers",
                column: "CurrentSessionId",
                principalTable: "CurrentUserSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryLanguage_Countries_CountryId",
                table: "CountryLanguage",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryLanguage_Languages_LanguageId",
                table: "CountryLanguage",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentUserSession_GeoLocations_GeoLocationId",
                table: "CurrentUserSession",
                column: "GeoLocationId",
                principalTable: "GeoLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentUserSession_ApplicationTenants_TenantId",
                table: "CurrentUserSession",
                column: "TenantId",
                principalTable: "ApplicationTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
