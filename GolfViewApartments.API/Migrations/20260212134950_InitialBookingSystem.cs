using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialBookingSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_ApartmentId",
                table: "Bookings");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MaxOccupancy",
                table: "RoomTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomRates",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "ConferencePackages",
                columns: new[] { "Id", "CreatedAt", "IconClass", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "fa-solid fa-sun", "Full Day Package", 2500m, null },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "fa-solid fa-cloud-sun", "Half Day Package", 1500m, null },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "fa-solid fa-bed-pulse", "Residential Package", 8000m, null }
                });

            migrationBuilder.InsertData(
                table: "FitnessAmenities",
                columns: new[] { "Id", "CreatedAt", "DayRate", "IconClass", "MonthlyRate", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 500m, "fa-solid fa-dumbbell", 5000m, "Gym", null },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 500m, "fa-solid fa-person-swimming", 5000m, "Swimming Pool", null },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1000m, "fa-solid fa-water", 0m, "Steam Bath", null },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1000m, "fa-solid fa-hot-tub-person", 0m, "Sauna", null }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "CreatedAt", "IconClass", "MaxOccupancy", "Name", "RoomTypeEnum" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "fa-solid fa-bed", 2, "Studio", 1 },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "fa-solid fa-door-open", 2, "One Bedroom", 2 },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "fa-solid fa-house", 4, "Two Bedroom", 3 }
                });

            migrationBuilder.InsertData(
                table: "RoomRates",
                columns: new[] { "Id", "BoardType", "CreatedAt", "FirstOccupancy", "RoomTypeId", "SecondOccupancy" },
                values: new object[,]
                {
                    { 1, "Bed Only", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5000m, 1, 7000m },
                    { 2, "Bed and Breakfast", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5500m, 1, 7500m },
                    { 3, "Half Board", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6000m, 1, 8000m },
                    { 4, "Full Board", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6500m, 1, 8500m },
                    { 5, "Bed Only", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6000m, 2, 8000m },
                    { 6, "Bed and Breakfast", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6500m, 2, 8500m },
                    { 7, "Half Board", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7000m, 2, 9000m },
                    { 8, "Full Board", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7500m, 2, 9500m },
                    { 9, "Bed Only", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8000m, 3, 10000m },
                    { 10, "Bed and Breakfast", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8500m, 3, 10500m },
                    { 11, "Half Board", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9000m, 3, 11000m },
                    { 12, "Full Board", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9500m, 3, 11500m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ApartmentId_CheckIn_CheckOut",
                table: "Bookings",
                columns: new[] { "ApartmentId", "CheckIn", "CheckOut" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingReference",
                table: "Bookings",
                column: "BookingReference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Status",
                table: "Bookings",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Email",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ApartmentId_CheckIn_CheckOut",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingReference",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_Status",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "ConferencePackages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ConferencePackages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ConferencePackages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FitnessAmenities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RoomRates",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "MaxOccupancy",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RoomRates");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ApartmentId",
                table: "Bookings",
                column: "ApartmentId");
        }
    }
}
