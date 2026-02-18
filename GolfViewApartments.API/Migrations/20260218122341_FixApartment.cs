using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class FixApartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DailyBB", "DailyBedOnly", "MonthlyBB", "MonthlyBedOnly" },
                values: new object[] { 0m, 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DailyBB", "DailyBedOnly", "MonthlyBB", "MonthlyBedOnly" },
                values: new object[] { 0m, 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DailyBB", "DailyBedOnly", "MonthlyBB", "MonthlyBedOnly" },
                values: new object[] { 0m, 0m, 0m, 0m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DailyBB", "DailyBedOnly", "MonthlyBB", "MonthlyBedOnly" },
                values: new object[] { 100m, 85m, 2100m, 1800m });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DailyBB", "DailyBedOnly", "MonthlyBB", "MonthlyBedOnly" },
                values: new object[] { 140m, 120m, 3200m, 2800m });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DailyBB", "DailyBedOnly", "MonthlyBB", "MonthlyBedOnly" },
                values: new object[] { 220m, 180m, 4800m, 4200m });
        }
    }
}
