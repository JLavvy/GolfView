using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bedrooms",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Apartments");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Bedrooms",
                table: "Apartments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Apartments",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Apartments",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Bedrooms", "Description", "Image" },
                values: new object[] { 0, "A cozy studio apartment ideal for solo travellers or couples.", "/images/studio.jpg" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bedrooms", "Description", "Image" },
                values: new object[] { 1, "A comfortable one bedroom apartment with a separate living area.", "/images/one-bedroom.jpg" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Bedrooms", "Description", "Image" },
                values: new object[] { 2, "A spacious two bedroom apartment perfect for families or groups.", "/images/two-bedroom.jpg" });
        }
    }
}
