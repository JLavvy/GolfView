using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomRatesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DailyBedOnly = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DailyBnB = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyBedOnly = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyBnB = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IconClass = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomRates", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$nr3KRX8m7MxGRhBF/diMQehtTx3SBJJ4d4BGq.T25izITRhEF5tl2");

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 26, 6, 27, 24, 44, DateTimeKind.Utc).AddTicks(2341));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 26, 6, 27, 24, 44, DateTimeKind.Utc).AddTicks(2343));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 26, 6, 27, 24, 44, DateTimeKind.Utc).AddTicks(2344));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 26, 6, 27, 24, 44, DateTimeKind.Utc).AddTicks(2345));

            migrationBuilder.UpdateData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 26, 6, 27, 24, 44, DateTimeKind.Utc).AddTicks(2306));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomRates");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$gwS9mpKsNk4fer0hhCA1rOJSduO/t0Ea.63UwKStig93jXEoIVuFW");

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 21, 59, 868, DateTimeKind.Utc).AddTicks(5685));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 21, 59, 868, DateTimeKind.Utc).AddTicks(5688));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 21, 59, 868, DateTimeKind.Utc).AddTicks(5690));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 21, 59, 868, DateTimeKind.Utc).AddTicks(5691));

            migrationBuilder.UpdateData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 21, 59, 868, DateTimeKind.Utc).AddTicks(5621));
        }
    }
}
