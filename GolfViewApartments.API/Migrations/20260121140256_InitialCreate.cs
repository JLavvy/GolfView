using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GolfViewApartments.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AmenityPricing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IconClass = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityPricing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApartmentId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Size = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MaxGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalUnits = table.Column<int>(type: "INTEGER", nullable: false),
                    DailyBedOnly = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DailyBB = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyBedOnly = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyBB = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    WhatsApp = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Website = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    FacebookUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    InstagramUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TwitterUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Subject = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ApartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Floor = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ApartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CheckIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Adults = table.Column<int>(type: "INTEGER", nullable: false),
                    Children = table.Column<int>(type: "INTEGER", nullable: false),
                    ChildrenAges = table.Column<string>(type: "TEXT", nullable: false),
                    RentalType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MealPlan = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialRequests = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AmenityPricing",
                columns: new[] { "Id", "IconClass", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "fa-solid fa-dumbbell", "Gym Access", 500m, new DateTime(2026, 1, 21, 14, 2, 56, 294, DateTimeKind.Utc).AddTicks(8200) },
                    { 2, "fa-solid fa-person-swimming", "Pool Access", 500m, new DateTime(2026, 1, 21, 14, 2, 56, 294, DateTimeKind.Utc).AddTicks(8202) },
                    { 3, "fa-solid fa-hot-tub-person", "Steam Bath", 1000m, new DateTime(2026, 1, 21, 14, 2, 56, 294, DateTimeKind.Utc).AddTicks(8204) },
                    { 4, "fa-solid fa-fire", "Sauna", 1000m, new DateTime(2026, 1, 21, 14, 2, 56, 294, DateTimeKind.Utc).AddTicks(8206) }
                });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "ApartmentId", "DailyBB", "DailyBedOnly", "MaxGuests", "MonthlyBB", "MonthlyBedOnly", "Name", "Size", "TotalUnits", "Type" },
                values: new object[,]
                {
                    { 1, "studio-apartment", 100m, 85m, 2, 2100m, 1800m, "Studio Apartment", "24 sqm", 13, "studio" },
                    { 2, "one-bedroom-apartment", 140m, 120m, 2, 3200m, 2800m, "One Bedroom Apartment", "30 sqm", 13, "one-bedroom" },
                    { 3, "two-bedroom-apartment", 220m, 180m, 4, 4800m, 4200m, "Two Bedroom Apartment", "40 sqm", 13, "two-bedroom" }
                });

            migrationBuilder.InsertData(
                table: "ContactInfo",
                columns: new[] { "Id", "Address", "Description", "Email", "FacebookUrl", "InstagramUrl", "Phone", "TwitterUrl", "UpdatedAt", "Website", "WhatsApp" },
                values: new object[] { 1, "Muchai Drive, off Ngong Road, Nairobi, Kenya", "Nestled in the quiet and tranquil Muchai drive off Ngong Road, Golfview provides secure first-class accommodation second to none!", "info@golfviewapartments.co.ke", "", "", "+254 700 000 000", "", new DateTime(2026, 1, 21, 14, 2, 56, 294, DateTimeKind.Utc).AddTicks(8168), "https://golfviewapartments.co.ke", "+254 700 000 000" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "ApartmentId", "Floor", "IsAvailable", "Number", "Type" },
                values: new object[,]
                {
                    { 1, 1, 0, true, "101", "Studio" },
                    { 2, 1, 0, true, "102", "Studio" },
                    { 3, 1, 1, true, "103", "Studio" },
                    { 4, 1, 1, true, "104", "Studio" },
                    { 5, 1, 2, true, "105", "Studio" },
                    { 6, 1, 2, true, "106", "Studio" },
                    { 7, 1, 3, true, "107", "Studio" },
                    { 8, 1, 3, true, "108", "Studio" },
                    { 9, 1, 4, true, "109", "Studio" },
                    { 10, 1, 4, true, "1010", "Studio" },
                    { 11, 1, 5, true, "1011", "Studio" },
                    { 12, 1, 5, true, "1012", "Studio" },
                    { 13, 1, 6, true, "1013", "Studio" },
                    { 14, 2, 0, true, "201", "1 Bedroom" },
                    { 15, 2, 0, true, "202", "1 Bedroom" },
                    { 16, 2, 1, true, "203", "1 Bedroom" },
                    { 17, 2, 1, true, "204", "1 Bedroom" },
                    { 18, 2, 2, true, "205", "1 Bedroom" },
                    { 19, 2, 2, true, "206", "1 Bedroom" },
                    { 20, 2, 3, true, "207", "1 Bedroom" },
                    { 21, 2, 3, true, "208", "1 Bedroom" },
                    { 22, 2, 4, true, "209", "1 Bedroom" },
                    { 23, 2, 4, true, "2010", "1 Bedroom" },
                    { 24, 2, 5, true, "2011", "1 Bedroom" },
                    { 25, 2, 5, true, "2012", "1 Bedroom" },
                    { 26, 2, 6, true, "2013", "1 Bedroom" },
                    { 27, 3, 0, true, "301", "2 Bedroom" },
                    { 28, 3, 0, true, "302", "2 Bedroom" },
                    { 29, 3, 1, true, "303", "2 Bedroom" },
                    { 30, 3, 1, true, "304", "2 Bedroom" },
                    { 31, 3, 2, true, "305", "2 Bedroom" },
                    { 32, 3, 2, true, "306", "2 Bedroom" },
                    { 33, 3, 3, true, "307", "2 Bedroom" },
                    { 34, 3, 3, true, "308", "2 Bedroom" },
                    { 35, 3, 4, true, "309", "2 Bedroom" },
                    { 36, 3, 4, true, "3010", "2 Bedroom" },
                    { 37, 3, 5, true, "3011", "2 Bedroom" },
                    { 38, 3, 5, true, "3012", "2 Bedroom" },
                    { 39, 3, 6, true, "3013", "2 Bedroom" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ApartmentId",
                table: "Bookings",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ApartmentId",
                table: "Rooms",
                column: "ApartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AmenityPricing");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "ContactInfo");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Apartments");
        }
    }
}
