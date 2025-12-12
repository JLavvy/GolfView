// Models/ApartmentModels.cs
namespace GolfViewApartments.Models
{
    public class Apartment
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int Units { get; set; }
        public int MaxGuests { get; set; }
        public string BedSize { get; set; } = string.Empty;
        public ApartmentPricing Pricing { get; set; } = new();
        public List<string> Features { get; set; } = new();
    }

    public class ApartmentPricing
    {
        public decimal DailyBedOnly { get; set; }
        public decimal DailyBB { get; set; }
        public decimal MonthlyBedOnly { get; set; }
        public decimal MonthlyBB { get; set; }
    }

    public class Feature
    {
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class MealPlan
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

// Data/ApartmentData.cs
namespace GolfViewApartments.Data
{
    using GolfViewApartments.Models;

    public static class ApartmentData
    {
        public static int TotalUnits => 39;

        public static List<Feature> Features => new()
        {
            new Feature
            {
                Icon = "home",
                Title = "39 Apartments",
                Description = "Studios, 1 & 2 bedroom apartments with golf course views."
            },
            new Feature
            {
                Icon = "bed",
                Title = "5x6 Beds",
                Description = "All rooms feature comfortable 5x6 beds for quality rest."
            },
            new Feature
            {
                Icon = "calendar",
                Title = "Daily & Monthly",
                Description = "Short stays or long-term rentals with special rates."
            },
            new Feature
            {
                Icon = "coffee",
                Title = "Bed & Breakfast",
                Description = "Bed only or B+B options available for all rooms."
            }
        };

        public static List<MealPlan> MealPlans => new()
        {
            new MealPlan { Name = "Bed Only", Description = "Room accommodation only" },
            new MealPlan { Name = "B+B", Description = "Room with breakfast included" }
        };

        public static List<Apartment> Apartments => new()
        {
            new Apartment
            {
                Id = "studio",
                Name = "Studio Apartment",
                Type = "studio",
                Size = "25m²",
                Description = "Cozy studio with golf course views, perfect for solo travelers or couples.",
                Image = "/images/studio.webp",
                Units = 15,
                MaxGuests = 2,
                BedSize = "5x6",
                Pricing = new ApartmentPricing
                {
                    DailyBedOnly = 3500,
                    DailyBB = 4500,
                    MonthlyBedOnly = 70000,
                    MonthlyBB = 90000
                },
                Features = new List<string> { "Kitchenette", "Private Bathroom", "WiFi", "Golf Views" }
            },
            new Apartment
            {
                Id = "one-bedroom",
                Name = "One Bedroom Apartment",
                Type = "one-bedroom",
                Size = "45m²",
                Description = "Spacious one bedroom with separate living area and stunning golf course views.",
                Image = "/images/1bedroom.webp",
                Units = 12,
                MaxGuests = 2,
                BedSize = "5x6",
                Pricing = new ApartmentPricing
                {
                    DailyBedOnly = 5000,
                    DailyBB = 6500,
                    MonthlyBedOnly = 100000,
                    MonthlyBB = 130000
                },
                Features = new List<string> { "Full Kitchen", "Living Room", "Private Bathroom", "Balcony" }
            },
            new Apartment
            {
                Id = "two-bedroom",
                Name = "Two Bedroom Apartment",
                Type = "two-bedroom",
                Size = "65m²",
                Description = "Luxurious two bedroom apartment ideal for families or extended stays.",
                Image = "/images/2bedroom.webp",
                Units = 12,
                MaxGuests = 4,
                BedSize = "5x6",
                Pricing = new ApartmentPricing
                {
                    DailyBedOnly = 7500,
                    DailyBB = 9500,
                    MonthlyBedOnly = 150000,
                    MonthlyBB = 190000
                },
                Features = new List<string> { "Full Kitchen", "2 Bathrooms", "Living & Dining", "Large Balcony" }
            }
        };
    }
}