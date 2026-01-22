using Microsoft.EntityFrameworkCore;
using GolfViewApartments.API.Models;

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
        public DbSet<AmenityPricing> AmenityPricing { get; set; }
        public DbSet<Admin> Admins { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Apartment)
                .WithMany(a => a.Bookings)
                .HasForeignKey(b => b.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Apartment)
                .WithMany(a => a.Rooms)
                .HasForeignKey(r => r.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed apartment data
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
                    DailyBedOnly = 85,
                    DailyBB = 100,
                    MonthlyBedOnly = 1800,
                    MonthlyBB = 2100
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
                    DailyBedOnly = 120,
                    DailyBB = 140,
                    MonthlyBedOnly = 2800,
                    MonthlyBB = 3200
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
                    DailyBedOnly = 180,
                    DailyBB = 220,
                    MonthlyBedOnly = 4200,
                    MonthlyBB = 4800
                }
            );

            // Seed rooms (Studios: 101-113, 1BR: 201-213, 2BR: 301-313)
            var rooms = new List<Room>();
            int roomId = 1;

            // Studio rooms
            for (int i = 1; i <= 13; i++)
            {
                rooms.Add(new Room
                {
                    Id = roomId++,
                    Number = $"10{i}",
                    ApartmentId = 1,
                    Type = "Studio",
                    Floor = (i - 1) / 2,
                    IsAvailable = true
                });
            }

            // 1 Bedroom rooms
            for (int i = 1; i <= 13; i++)
            {
                rooms.Add(new Room
                {
                    Id = roomId++,
                    Number = $"20{i}",
                    ApartmentId = 2,
                    Type = "1 Bedroom",
                    Floor = (i - 1) / 2,
                    IsAvailable = true
                });
            }

            // 2 Bedroom rooms
            for (int i = 1; i <= 13; i++)
            {
                rooms.Add(new Room
                {
                    Id = roomId++,
                    Number = $"30{i}",
                    ApartmentId = 3,
                    Type = "2 Bedroom",
                    Floor = (i - 1) / 2,
                    IsAvailable = true
                });
            }

            modelBuilder.Entity<Room>().HasData(rooms);

            // Seed contact info
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
                    UpdatedAt = DateTime.UtcNow
                }
            );

            // Seed amenity pricing
            modelBuilder.Entity<AmenityPricing>().HasData(
                new AmenityPricing { Id = 1, Name = "Gym Access", IconClass = "fa-solid fa-dumbbell", Price = 500, UpdatedAt = DateTime.UtcNow },
                new AmenityPricing { Id = 2, Name = "Pool Access", IconClass = "fa-solid fa-person-swimming", Price = 500, UpdatedAt = DateTime.UtcNow },
                new AmenityPricing { Id = 3, Name = "Steam Bath", IconClass = "fa-solid fa-hot-tub-person", Price = 1000, UpdatedAt = DateTime.UtcNow },
                new AmenityPricing { Id = 4, Name = "Sauna", IconClass = "fa-solid fa-fire", Price = 1000, UpdatedAt = DateTime.UtcNow }
            );

            modelBuilder.Entity<Admin>().HasData(
        new Admin
        {
            Id = 1,
            Email = "admin@golfview.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("$Admin123!"),
            Role = "Admin"
        }
    );
        }



    }
}