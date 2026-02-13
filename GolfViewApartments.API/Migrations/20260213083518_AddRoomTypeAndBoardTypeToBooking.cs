using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomTypeAndBoardTypeToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomNumber",
                table: "Bookings",
                newName: "Room");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ApartmentId_CheckIn_CheckOut",
                table: "Bookings",
                newName: "IX_Bookings_Apartment_Dates");

            migrationBuilder.AlterColumn<string>(
                name: "RoomTypeEnum",
                table: "RoomTypes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "BoardType",
                table: "Bookings",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Occupancy",
                table: "Bookings",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoomTypeEnum",
                value: "Studio");

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoomTypeEnum",
                value: "OneBedroom");

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoomTypeEnum",
                value: "TwoBedroom");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CreatedAt",
                table: "Bookings",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Room_Dates",
                table: "Bookings",
                columns: new[] { "Room", "CheckIn", "CheckOut" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomType_Dates",
                table: "Bookings",
                columns: new[] { "RoomType", "CheckIn", "CheckOut" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_CreatedAt",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_Room_Dates",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomType_Dates",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BoardType",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Occupancy",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Room",
                table: "Bookings",
                newName: "RoomNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_Apartment_Dates",
                table: "Bookings",
                newName: "IX_Bookings_ApartmentId_CheckIn_CheckOut");

            migrationBuilder.AlterColumn<int>(
                name: "RoomTypeEnum",
                table: "RoomTypes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoomTypeEnum",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoomTypeEnum",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoomTypeEnum",
                value: 3);
        }
    }
}
