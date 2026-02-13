using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPricingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "DailyBedOnly",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "DailyBnB",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RoomRates");

            migrationBuilder.RenameColumn(
                name: "MonthlyBnB",
                table: "RoomRates",
                newName: "SecondOccupancy");

            migrationBuilder.RenameColumn(
                name: "MonthlyBedOnly",
                table: "RoomRates",
                newName: "FirstOccupancy");

            migrationBuilder.RenameColumn(
                name: "IconClass",
                table: "RoomRates",
                newName: "BoardType");

            migrationBuilder.AddColumn<int>(
                name: "RoomTypeId",
                table: "RoomRates",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BookingReference",
                table: "Bookings",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConferencePackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IconClass = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferencePackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FitnessAmenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IconClass = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DayRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessAmenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    RoomTypeEnum = table.Column<int>(type: "INTEGER", nullable: false),
                    IconClass = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$xuCV2ywEeNc2y1MdD3uldOAf38pRqDTaHjvON.LpC8Xfd.l0xmpIu");

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 2, 6, 8, 50, 43, 722, DateTimeKind.Utc).AddTicks(5483));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 2, 6, 8, 50, 43, 722, DateTimeKind.Utc).AddTicks(5484));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 2, 6, 8, 50, 43, 722, DateTimeKind.Utc).AddTicks(5486));

            migrationBuilder.UpdateData(
                table: "AmenityPricing",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 2, 6, 8, 50, 43, 722, DateTimeKind.Utc).AddTicks(5487));

            migrationBuilder.UpdateData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 2, 6, 8, 50, 43, 722, DateTimeKind.Utc).AddTicks(5399));

            migrationBuilder.CreateIndex(
                name: "IX_RoomRates_RoomTypeId",
                table: "RoomRates",
                column: "RoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomRates_RoomTypes_RoomTypeId",
                table: "RoomRates",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomRates_RoomTypes_RoomTypeId",
                table: "RoomRates");

            migrationBuilder.DropTable(
                name: "ConferencePackages");

            migrationBuilder.DropTable(
                name: "FitnessAmenities");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_RoomRates_RoomTypeId",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "RoomTypeId",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "BookingReference",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "SecondOccupancy",
                table: "RoomRates",
                newName: "MonthlyBnB");

            migrationBuilder.RenameColumn(
                name: "FirstOccupancy",
                table: "RoomRates",
                newName: "MonthlyBedOnly");

            migrationBuilder.RenameColumn(
                name: "BoardType",
                table: "RoomRates",
                newName: "IconClass");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomRates",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "DailyBedOnly",
                table: "RoomRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DailyBnB",
                table: "RoomRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RoomRates",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RoomRates",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
