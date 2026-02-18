using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class FixRoomNumbersToMatchLayout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Number",
                value: "G01");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApartmentId", "Number", "Type" },
                values: new object[] { 2, "G02", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 0, "G03", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "Number",
                value: "101");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 1, "102", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 1, "103", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 1, "104" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 1, "105", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 1, "106", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 2, "201" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 2, "202", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 2, "203", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 2, "204" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 2, "205" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 2, "206", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 3, "301", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 3, "302" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 3, "303", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 3, "304", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 20,
                column: "Number",
                value: "305");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ApartmentId", "Number", "Type" },
                values: new object[] { 3, "306", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ApartmentId", "Number", "Type" },
                values: new object[] { 1, "401", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 23,
                column: "Number",
                value: "402");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 4, "403", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 4, "404", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 4, "405" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 4, "406" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 5, "501", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 5, "502", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 5, "503" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 5, "504", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 5, "505", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 5, "506" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 6, "601", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 6, "602", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 6, "603" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 6, "604", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 6, "605", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 39,
                column: "Number",
                value: "606");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Number",
                value: "101");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApartmentId", "Number", "Type" },
                values: new object[] { 1, "102", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 1, "103", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "Number",
                value: "104");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 2, "105", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 2, "106", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 3, "107" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 3, "108", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 4, "109", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 4, "1010" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 5, "1011", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 1, 5, "1012", "Studio" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 6, "1013" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 0, "201" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 0, "202", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 1, "203", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 1, "204" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 2, "205", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 2, "206", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 20,
                column: "Number",
                value: "207");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ApartmentId", "Number", "Type" },
                values: new object[] { 2, "208", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ApartmentId", "Number", "Type" },
                values: new object[] { 2, "209", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 23,
                column: "Number",
                value: "2010");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 5, "2011", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 2, 5, "2012", "1 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 6, "2013" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 0, "301" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 0, "302", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 1, "303", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 1, "304" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 2, "305", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 2, "306", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 3, "307" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 3, "308", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 4, "309", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Floor", "Number" },
                values: new object[] { 4, "3010" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 5, "3011", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ApartmentId", "Floor", "Number", "Type" },
                values: new object[] { 3, 5, "3012", "2 Bedroom" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 39,
                column: "Number",
                value: "3013");
        }
    }
}
