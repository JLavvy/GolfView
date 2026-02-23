using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOfficeHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MondayFridayHours", "SaturdayHours", "SundayHours" },
                values: new object[] { "8:00 AM - 6:00 PM", "9:00 AM - 5:00 PM", "By Appointment Only" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MondayFridayHours", "SaturdayHours", "SundayHours" },
                values: new object[] { "", "", "" });
        }
    }
}
