using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCoreAppFramework.Migrations
{
    public partial class VisibleSystemFields2RelevantEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetName",
                table: "PostalCodes",
                newName: "LocationName");

            migrationBuilder.AddColumn<bool>(
                name: "System",
                table: "PostalCodes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "PostalCodes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "System",
                table: "Languages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Languages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "System",
                table: "GeoLocations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "GeoLocations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "System",
                table: "Districs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Districs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "System",
                table: "CountryLanguage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "CountryLanguage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "System",
                table: "Countries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Countries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "System",
                table: "Counties",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Counties",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "System",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "System",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "System",
                table: "ApplicationTenants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "ApplicationTenants",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "System",
                table: "PostalCodes");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "PostalCodes");

            migrationBuilder.DropColumn(
                name: "System",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "System",
                table: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "System",
                table: "Districs");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Districs");

            migrationBuilder.DropColumn(
                name: "System",
                table: "CountryLanguage");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "CountryLanguage");

            migrationBuilder.DropColumn(
                name: "System",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "System",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "System",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "System",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "System",
                table: "ApplicationTenants");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "ApplicationTenants");

            migrationBuilder.RenameColumn(
                name: "LocationName",
                table: "PostalCodes",
                newName: "StreetName");
        }
    }
}
