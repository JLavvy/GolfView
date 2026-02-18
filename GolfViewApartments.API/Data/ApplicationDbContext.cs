using Microsoft.EntityFrameworkCore;
using GolfViewApartments.API.Models;
using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<RoomRate> RoomRates { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<ConferencePackage> ConferencePackages { get; set; }
        public DbSet<FitnessAmenity> FitnessAmenities { get; set; }
        public DbSet<AmenityPricing> AmenityPricing { get; set; }
        public DbSet<Admin> Admins { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================================
            // BOOKING CONFIGURATION (UPDATED)
            // ========================================
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BookingReference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Room)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RoomType)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(50);

                entity.Property(e => e.BoardType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Occupancy)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CheckIn)
                    .IsRequired();

                entity.Property(e => e.CheckOut)
                    .IsRequired();

                entity.Property(e => e.Adults)
                    .IsRequired();

                entity.Property(e => e.Children)
                    .IsRequired();

                entity.Property(e => e.ChildrenAges)
                    .HasMaxLength(200);

                entity.Property(e => e.TotalPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.SpecialRequests)
                    .HasMaxLength(1000);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Bookings)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Apartment)
                    .WithMany(a => a.Bookings)
                    .HasForeignKey(e => e.ApartmentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.BookingReference)
                    .IsUnique()
                    .HasDatabaseName("IX_Bookings_BookingReference");

                entity.HasIndex(e => e.CustomerId)
                    .HasDatabaseName("IX_Bookings_CustomerId");

                entity.HasIndex(e => new { e.ApartmentId, e.CheckIn, e.CheckOut })
                    .HasDatabaseName("IX_Bookings_Apartment_Dates");

                entity.HasIndex(e => new { e.RoomType, e.CheckIn, e.CheckOut })
                    .HasDatabaseName("IX_Bookings_RoomType_Dates");

                entity.HasIndex(e => new { e.Room, e.CheckIn, e.CheckOut })
                    .HasDatabaseName("IX_Bookings_Room_Dates");

                entity.HasIndex(e => e.Status)
                    .HasDatabaseName("IX_Bookings_Status");

                entity.HasIndex(e => e.CreatedAt)
                    .HasDatabaseName("IX_Bookings_CreatedAt");
            });

            // ========================================
            // ROOM CONFIGURATION
            // ========================================
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Apartment)
                .WithMany(a => a.Rooms)
                .HasForeignKey(r => r.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // ========================================
            // CUSTOMER CONFIGURATION
            // ========================================
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            // ========================================
            // ROOM TYPE CONFIGURATION
            // ========================================
            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.RoomTypeEnum).IsRequired().HasConversion<string>();
                entity.HasMany(e => e.Rates)
                    .WithOne(e => e.RoomType)
                    .HasForeignKey(e => e.RoomTypeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ========================================
            // ROOM RATE CONFIGURATION
            // ========================================
            modelBuilder.Entity<RoomRate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BoardType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.FirstOccupancy).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SecondOccupancy).HasColumnType("decimal(18,2)");
            });

            // ========================================
            // CONFERENCE PACKAGE CONFIGURATION
            // ========================================
            modelBuilder.Entity<ConferencePackage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });

            // ========================================
            // FITNESS AMENITY CONFIGURATION
            // ========================================
            modelBuilder.Entity<FitnessAmenity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DayRate).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MonthlyRate).HasColumnType("decimal(18,2)");
            });

            // ========================================
            // SEED DATA: APARTMENTS
            // ========================================
            modelBuilder.Entity<Apartment>().HasData(
                new Apartment
                {
                    Id = 1,
                    ApartmentId = "studio-apartment",
                    Name = "Studio Apartment",
                    Type = "studio",
                    Size = "24 sqm",
                    MaxGuests = 2,
                    TotalUnits = 13,
                   
                },
                new Apartment
                {
                    Id = 2,
                    ApartmentId = "one-bedroom-apartment",
                    Name = "One Bedroom Apartment",
                    Type = "one-bedroom",
                    Size = "30 sqm",
                    MaxGuests = 2,
                    TotalUnits = 13,
                    
                },
                new Apartment
                {
                    Id = 3,
                    ApartmentId = "two-bedroom-apartment",
                    Name = "Two Bedroom Apartment",
                    Type = "two-bedroom",
                    Size = "40 sqm",
                    MaxGuests = 4,
                    TotalUnits = 13,
                 
                }
            );

            // ========================================
            // SEED DATA: ROOMS
            // ========================================
            // Mirrors ApartmentData.GetBuildingLayout() exactly:
            // 7 floors (0=Ground, 1-6=upper)
            // FloorDistribution = [1, 2, 2, 2, 2, 2, 2] per type
            // Units interleaved per floor: Studio(0), 1-Bed(1), 2-Bed(2) via i % 3
            // Room number = floorPrefix + unitCounter (D2)
            //
            // Result:
            //   Ground: G01(S), G02(1B), G03(2B)
            //   Floor1: 101(S), 102(1B), 103(2B), 104(S), 105(1B), 106(2B)
            //   Floor2: 201(S), 202(1B), 203(2B), 204(S), 205(1B), 206(2B)
            //   ... same pattern for floors 3-6

            var rooms = new List<Room>();
            int roomId = 1;

            var floorDistribution = new[] { 1, 2, 2, 2, 2, 2, 2 }; // units per type per floor

            for (int floorIndex = 0; floorIndex < 7; floorIndex++)
            {
                var floorPrefix = floorIndex == 0 ? "G" : floorIndex.ToString();
                var unitsPerType = floorDistribution[floorIndex];
                var totalUnits = unitsPerType * 3;

                var unitCounter = 1;
                var studioIdx = 0;
                var oneBedroomIdx = 0;
                var twoBedroomIdx = 0;

                for (int i = 0; i < totalUnits; i++)
                {
                    var unitType = i % 3;
                    var roomNumber = $"{floorPrefix}{unitCounter:D2}";

                    if (unitType == 0 && studioIdx < unitsPerType)
                    {
                        rooms.Add(new Room { Id = roomId++, Number = roomNumber, ApartmentId = 1, Type = "Studio", Floor = floorIndex, IsAvailable = true });
                        studioIdx++; unitCounter++;
                    }
                    else if (unitType == 1 && oneBedroomIdx < unitsPerType)
                    {
                        rooms.Add(new Room { Id = roomId++, Number = roomNumber, ApartmentId = 2, Type = "1 Bedroom", Floor = floorIndex, IsAvailable = true });
                        oneBedroomIdx++; unitCounter++;
                    }
                    else if (unitType == 2 && twoBedroomIdx < unitsPerType)
                    {
                        rooms.Add(new Room { Id = roomId++, Number = roomNumber, ApartmentId = 3, Type = "2 Bedroom", Floor = floorIndex, IsAvailable = true });
                        twoBedroomIdx++; unitCounter++;
                    }
                    else
                    {
                        i--; // type exhausted, retry
                    }
                }
            }

            modelBuilder.Entity<Room>().HasData(rooms);

            // ========================================
            // SEED DATA: CONTACT INFO
            // ========================================
            modelBuilder.Entity<ContactInfo>().HasData(
                new ContactInfo
                {
                    Id = 1,
                    Address = "Muchai Drive, off Ngong Road, Nairobi, Kenya",
                    Phone = "+254 700 000 000",
                    Email = "info@golfviewapartments.co.ke",
                    WhatsApp = "+254 700 000 000",
                    Website = "https://golfviewapartments.co.ke",
                    Description = "Nestled in the quiet and tranquil Muchai drive off Ngong Road, Golfview provides secure first-class accommodation second to none!",
                    FacebookUrl = "",
                    InstagramUrl = "",
                    TwitterUrl = "",
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // ========================================
            // SEED DATA: AMENITY PRICING
            // ========================================
            modelBuilder.Entity<AmenityPricing>().HasData(
                new AmenityPricing { Id = 1, Name = "Gym Access", IconClass = "fa-solid fa-dumbbell", Price = 500, UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new AmenityPricing { Id = 2, Name = "Pool Access", IconClass = "fa-solid fa-person-swimming", Price = 500, UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new AmenityPricing { Id = 3, Name = "Steam Bath", IconClass = "fa-solid fa-hot-tub-person", Price = 1000, UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new AmenityPricing { Id = 4, Name = "Sauna", IconClass = "fa-solid fa-fire", Price = 1000, UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            // ========================================
            // SEED DATA: ROOM TYPES
            // ========================================
            modelBuilder.Entity<RoomType>().HasData(
                new RoomType
                {
                    Id = 1,
                    Name = "Studio",
                    RoomTypeEnum = RoomTypeEnum.Studio,
                    IconClass = "fa-solid fa-bed",
                    MaxOccupancy = 2,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new RoomType
                {
                    Id = 2,
                    Name = "One Bedroom",
                    RoomTypeEnum = RoomTypeEnum.OneBedroom,
                    IconClass = "fa-solid fa-door-open",
                    MaxOccupancy = 2,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new RoomType
                {
                    Id = 3,
                    Name = "Two Bedroom",
                    RoomTypeEnum = RoomTypeEnum.TwoBedroom,
                    IconClass = "fa-solid fa-house",
                    MaxOccupancy = 4,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // ========================================
            // SEED DATA: ROOM RATES
            // ========================================
            var roomRates = new List<RoomRate>();
            var rateId = 1;
            var boardTypes = new[] { "Bed Only", "Bed and Breakfast", "Half Board", "Full Board" };

            // Studio rates
            foreach (var boardType in boardTypes)
            {
                roomRates.Add(new RoomRate
                {
                    Id = rateId++,
                    RoomTypeId = 1,
                    BoardType = boardType,
                    FirstOccupancy = 5000m + (Array.IndexOf(boardTypes, boardType) * 500m),
                    SecondOccupancy = 7000m + (Array.IndexOf(boardTypes, boardType) * 500m),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });
            }

            // One Bedroom rates
            foreach (var boardType in boardTypes)
            {
                roomRates.Add(new RoomRate
                {
                    Id = rateId++,
                    RoomTypeId = 2,
                    BoardType = boardType,
                    FirstOccupancy = 6000m + (Array.IndexOf(boardTypes, boardType) * 500m),
                    SecondOccupancy = 8000m + (Array.IndexOf(boardTypes, boardType) * 500m),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });
            }

            // Two Bedroom rates
            foreach (var boardType in boardTypes)
            {
                roomRates.Add(new RoomRate
                {
                    Id = rateId++,
                    RoomTypeId = 3,
                    BoardType = boardType,
                    FirstOccupancy = 8000m + (Array.IndexOf(boardTypes, boardType) * 500m),
                    SecondOccupancy = 10000m + (Array.IndexOf(boardTypes, boardType) * 500m),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });
            }

            modelBuilder.Entity<RoomRate>().HasData(roomRates);

            // ========================================
            // SEED DATA: CONFERENCE PACKAGES
            // ========================================
            modelBuilder.Entity<ConferencePackage>().HasData(
                new ConferencePackage
                {
                    Id = 1,
                    Name = "Full Day Package",
                    IconClass = "fa-solid fa-sun",
                    Price = 2500m,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new ConferencePackage
                {
                    Id = 2,
                    Name = "Half Day Package",
                    IconClass = "fa-solid fa-cloud-sun",
                    Price = 1500m,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new ConferencePackage
                {
                    Id = 3,
                    Name = "Residential Package",
                    IconClass = "fa-solid fa-bed-pulse",
                    Price = 8000m,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // ========================================
            // SEED DATA: FITNESS AMENITIES
            // ========================================
            modelBuilder.Entity<FitnessAmenity>().HasData(
                new FitnessAmenity
                {
                    Id = 1,
                    Name = "Gym Only",
                    IconClass = "fa-solid fa-dumbbell",
                    DayRate = 500m,
                    MonthlyRate = 5000m,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new FitnessAmenity
                {
                    Id = 2,
                    Name = "Gym and Pool",
                    IconClass = "fa-solid fa-dumbbell",
                    DayRate = 500m,
                    MonthlyRate = 5000m,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                 new FitnessAmenity
                {
                    Id = 3,
                    Name = "Steam and Sauna (1hr Session)",
                    IconClass = "fa-solid fa-water",
                    DayRate = 1000m,
                    MonthlyRate = 0m,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new FitnessAmenity
                {
                    Id = 4,
                    Name = "Pool, Steam and Sauna",
                    IconClass = "fa-solid fa-person-swimming",
                    DayRate = 500m,
                    MonthlyRate = 5000m,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
               
                new FitnessAmenity
                {
                    Id = 5,
                    Name = "Gym, Pool, Steam and Sauna",
                    IconClass = "fa-solid fa-hot-tub-person",
                    DayRate = 1000m,
                    MonthlyRate = 5000m,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
