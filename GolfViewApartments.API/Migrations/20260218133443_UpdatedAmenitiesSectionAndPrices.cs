using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAmenitiesSectionAndPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Gym Only");

            migrationBuilder.UpdateData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IconClass", "Name" },
                values: new object[] { "fa-solid fa-dumbbell", "Gym and Pool" });

            migrationBuilder.UpdateData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Steam and Sauna");

            migrationBuilder.UpdateData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DayRate", "IconClass", "MonthlyRate", "Name" },
                values: new object[] { 500m, "fa-solid fa-person-swimming", 5000m, "Pool, Steam and Sauna" });

            migrationBuilder.InsertData(
                table: "FitnessAmenities",
                columns: new[] { "Id", "CreatedAt", "DayRate", "IconClass", "MonthlyRate", "Name", "UpdatedAt" },
                values: new object[] { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1000m, "fa-solid fa-hot-tub-person", 5000m, "Gym, Pool, Steam and Sauna", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Gym");

            migrationBuilder.UpdateData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IconClass", "Name" },
                values: new object[] { "fa-solid fa-person-swimming", "Swimming Pool" });

            migrationBuilder.UpdateData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Steam Bath");

            migrationBuilder.UpdateData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DayRate", "IconClass", "MonthlyRate", "Name" },
                values: new object[] { 1000m, "fa-solid fa-hot-tub-person", 0m, "Sauna" });
        }
    }
}
