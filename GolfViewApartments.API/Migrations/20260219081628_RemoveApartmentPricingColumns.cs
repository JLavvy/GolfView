using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveApartmentPricingColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyBB",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "DailyBedOnly",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "MonthlyBB",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "MonthlyBedOnly",
                table: "Apartments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DailyBB",
                table: "Apartments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DailyBedOnly",
                table: "Apartments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyBB",
                table: "Apartments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyBedOnly",
                table: "Apartments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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
    }
}
