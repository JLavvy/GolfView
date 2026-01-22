using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[] { 1, "admin@golfview.com", "$2a$11$NfhNvNMSTLG8yFJ3hZW1a.cItvpqR5Py8b0Me3FKOF56/PMdahci6", "Admin" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 2, 56, 294, DateTimeKind.Utc).AddTicks(8200));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 2, 56, 294, DateTimeKind.Utc).AddTicks(8202));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 2, 56, 294, DateTimeKind.Utc).AddTicks(8204));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 2, 56, 294, DateTimeKind.Utc).AddTicks(8206));

            migrationBuilder.UpdateData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 21, 14, 2, 56, 294, DateTimeKind.Utc).AddTicks(8168));
        }
    }
}
