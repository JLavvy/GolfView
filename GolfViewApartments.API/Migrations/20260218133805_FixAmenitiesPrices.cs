using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class FixAmenitiesPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Steam and Sauna (1hr Session)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Steam and Sauna");
        }
    }
}
