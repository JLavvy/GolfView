using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOfficeHoursToContactInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MondayFridayHours",
                table: "ContactInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SaturdayHours",
                table: "ContactInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SundayHours",
                table: "ContactInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MondayFridayHours", "SaturdayHours", "SundayHours" },
                values: new object[] { "", "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MondayFridayHours",
                table: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "SaturdayHours",
                table: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "SundayHours",
                table: "ContactInfo");
        }
    }
}
