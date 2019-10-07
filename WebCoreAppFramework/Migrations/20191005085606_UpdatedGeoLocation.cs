using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCoreAppFramework.Migrations
{
    public partial class UpdatedGeoLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GeoLocations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isCity",
                table: "GeoLocations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "Counties",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Counties_CountryId",
                table: "Counties",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Counties_Countries_CountryId",
                table: "Counties",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counties_Countries_CountryId",
                table: "Counties");

            migrationBuilder.DropIndex(
                name: "IX_Counties_CountryId",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "isCity",
                table: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Counties");
        }
    }
}
