using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class FixAdminPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$NfhNvNMSTLG8yFJ3hZW1a.cItvpqR5Py8b0Me3FKOF56/PMdahci6");

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 8, 43, 931, DateTimeKind.Utc).AddTicks(7861));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 8, 43, 931, DateTimeKind.Utc).AddTicks(7863));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 8, 43, 931, DateTimeKind.Utc).AddTicks(7864));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 8, 43, 931, DateTimeKind.Utc).AddTicks(7865));

            migrationBuilder.UpdateData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 8, 43, 931, DateTimeKind.Utc).AddTicks(7810));
        }
    }
}
