using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;
using Microsoft.EntityFrameworkCore;
using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.API.Services
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Seed Room Types and Rates
            if (!await _context.RoomTypes.AnyAsync())
            {
                var roomTypes = new List<RoomType>
                {
                    new RoomType
                    {
                        Name = "Studio Apartment",
                        RoomTypeEnum = RoomTypeEnum.Studio,
                        IconClass = "fa-solid fa-house-chimney",
                        Rates = new List<RoomRate>
                        {
                            new RoomRate { BoardType = "Bed Only", FirstOccupancy = 5000, SecondOccupancy = 5000 },
                            new RoomRate { BoardType = "Bed and Breakfast", FirstOccupancy = 6500, SecondOccupancy = 8000 },
                            new RoomRate { BoardType = "Half Board", FirstOccupancy = 8500, SecondOccupancy = 12000 },
                            new RoomRate { BoardType = "Full Board", FirstOccupancy = 10500, SecondOccupancy = 16000 }
                        }
                    },
                    new RoomType
                    {
                        Name = "One Bedroom Apartment",
                        RoomTypeEnum = RoomTypeEnum.OneBedroom,
                        IconClass = "fa-solid fa-house-chimney",
                        Rates = new List<RoomRate>
                        {
                            new RoomRate { BoardType = "Bed Only", FirstOccupancy = 7000, SecondOccupancy = 7000 },
                            new RoomRate { BoardType = "Bed and Breakfast", FirstOccupancy = 8500, SecondOccupancy = 10000 },
                            new RoomRate { BoardType = "Half Board", FirstOccupancy = 10500, SecondOccupancy = 14000 },
                            new RoomRate { BoardType = "Full Board", FirstOccupancy = 12500, SecondOccupancy = 18000 }
                        }
                    },
                    new RoomType
                    {
                        Name = "Two Bedroom Apartment",
                        RoomTypeEnum = RoomTypeEnum.TwoBedroom,
                        IconClass = "fa-solid fa-house-chimney",
                        Rates = new List<RoomRate>
                        {
                            new RoomRate { BoardType = "Bed Only", FirstOccupancy = 10000, SecondOccupancy = 10000 },
                            new RoomRate { BoardType = "Bed and Breakfast", FirstOccupancy = 13000, SecondOccupancy = 16000 },
                            new RoomRate { BoardType = "Half Board", FirstOccupancy = 17000, SecondOccupancy = 24000 },
                            new RoomRate { BoardType = "Full Board", FirstOccupancy = 21000, SecondOccupancy = 32000 }
                        }
                    }
                };

                await _context.RoomTypes.AddRangeAsync(roomTypes);
                await _context.SaveChangesAsync();
            }

            // Seed Conference Packages
            if (!await _context.ConferencePackages.AnyAsync())
            {
                var conferencePackages = new List<ConferencePackage>
                {
                    new ConferencePackage
                    {
                        Name = "Full Day",
                        IconClass = "fa-solid fa-calendar-day",
                        Price = 3500,
                        CreatedAt = DateTime.UtcNow
                    },
                    new ConferencePackage
                    {
                        Name = "Half Day",
                        IconClass = "fa-solid fa-clock",
                        Price = 2500,
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await _context.ConferencePackages.AddRangeAsync(conferencePackages);
                await _context.SaveChangesAsync();
            }

            // Seed Fitness Amenities
            if (!await _context.FitnessAmenities.AnyAsync())
            {
                var fitnessAmenities = new List<FitnessAmenity>
                {
                    new FitnessAmenity
                    {
                        Name = "Gym Only",
                        IconClass = "fa-solid fa-dumbbell",
                        DayRate = 1000,
                        MonthlyRate = 5000,
                        CreatedAt = DateTime.UtcNow
                    },
                    new FitnessAmenity
                    {
                        Name = "Gym and Pool",
                        IconClass = "fa-solid fa-person-swimming",
                        DayRate = 2000,
                        MonthlyRate = 10000,
                        CreatedAt = DateTime.UtcNow
                    },
                    new FitnessAmenity
                    {
                        Name = "Steam and Sauna (1hr session)",
                        IconClass = "fa-solid fa-hot-tub-person",
                        DayRate = 1000,
                        MonthlyRate = 0,
                        CreatedAt = DateTime.UtcNow
                    },
                    new FitnessAmenity
                    {
                        Name = "Pool, Steam and Sauna",
                        IconClass = "fa-solid fa-water-ladder",
                        DayRate = 3000,
                        MonthlyRate = 8000,
                        CreatedAt = DateTime.UtcNow
                    },
                    new FitnessAmenity
                    {
                        Name = "Gym, Pool, Steam and Sauna",
                        IconClass = "fa-solid fa-spa",
                        DayRate = 4000,
                        MonthlyRate = 13000,
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await _context.FitnessAmenities.AddRangeAsync(fitnessAmenities);
                await _context.SaveChangesAsync();
            }
        }
    }
}