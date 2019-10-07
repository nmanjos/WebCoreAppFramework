using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCoreAppFramework.Migrations
{
    public partial class UpdatedGeoLocation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "GeoLocations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeoLocations_CountryId",
                table: "GeoLocations",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeoLocations_Countries_CountryId",
                table: "GeoLocations",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeoLocations_Countries_CountryId",
                table: "GeoLocations");

            migrationBuilder.DropIndex(
                name: "IX_GeoLocations_CountryId",
                table: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "GeoLocations");
        }
    }
}
