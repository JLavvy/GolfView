namespace GolfViewApartments.Data
{
    using GolfViewApartments.Models;

    public static class ApartmentData
    {
        public static int TotalUnits => 39;
        public static int TotalFloors => 7; // Ground + 6 upper floors

        public static Dictionary<string, MealPlanInfo> MealPlans => new()
        {
            {
                "bed-only",
                new MealPlanInfo
                {
                    Label = "Bed Only",
                    ShortLabel = "B/O",
                    Description = "Room accommodation only"
                }
            },
            {
                "bed-breakfast",
                new MealPlanInfo
                {
                    Label = "Bed & Breakfast",
                    ShortLabel = "B+B",
                    Description = "Includes daily breakfast"
                }
            }
        };

        public static Dictionary<string, RentalTypeInfo> RentalTypes => new()
        {
            {
                "daily",
                new RentalTypeInfo
                {
                    Label = "Daily Rental",
                    Description = "Short-term stays with flexible dates"
                }
            },
            {
                "monthly",
                new RentalTypeInfo
                {
                    Label = "Monthly Rental",
                    Description = "Long-term stays with special rates"
                }
            }
        };

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

        public static List<Apartment> Apartments => new()
        {
            new Apartment
            {
                Id = "studio-apartment",
                Name = "Studio Apartment",
                Type = "studio",
                Size = "24 sqm",
                Description = "Cozy studio apartment with stunning golf course views, fully equipped kitchenette, and modern amenities. Perfect for solo travelers or couples seeking a comfortable retreat.",
                LongDescription = "Our Studio Apartment offers the perfect blend of comfort and elegance. Wake up to breathtaking views of the championship golf course from your comfortable 5x6 bed. The compact 24sqm open-plan design features a fully equipped kitchenette, a comfortable living area, and a modern bathroom with premium amenities. Ideal for solo travelers or couples seeking a peaceful retreat.",
                Image = "/images/studio.webp",
                // Total Units updated to 13 (1 on Ground, 2 on Floors 1-6)
                Units = 13, 
                MaxGuests = 2,
                BedSize = "5x6",
                Bedrooms = "Studio",
                Bathrooms = 1,
                // Ground Floor (Index 0) = 1 unit, Floors 1-6 (Indices 1-6) = 2 units each
                FloorDistribution = new List<int> { 1, 2, 2, 2, 2, 2, 2 }, 
                Pricing = new ApartmentPricing
                {
                    DailyBedOnly = 85,
                    DailyBB = 100,
                    MonthlyBedOnly = 1800,
                    MonthlyBB = 2100
                },
                Amenities = new List<string>
                {
                    "High-Speed WiFi", "Air Conditioning", "42\" Smart TV",
                    "Kitchenette", "Coffee Maker", "Mini Fridge",
                    "In-room Safe", "Hair Dryer", "Premium Toiletries"
                },
                Features = new List<string>
                {
                    "Golf Course View", "5x6 Double Bed", "Daily Housekeeping",
                    "24/7 Reception", "Pool Access", "Gym Access"
                }
            },
            new Apartment
            {
                Id = "one-bedroom-apartment",
                Name = "One Bedroom Apartment",
                Type = "one-bedroom",
                Size = "30 sqm",
                Description = "Spacious one-bedroom apartment with separate living area, full kitchen, and premium furnishings. Ideal for couples or solo travelers looking for extra space.",
                LongDescription = "The One Bedroom Apartment provides generous 30sqm space for extended stays or those who appreciate extra room. A separate bedroom with a premium 5x6 king-size bed offers privacy, while the living area features a comfortable sofa, work desk, and dining space. The full kitchen allows you to prepare meals at your convenience. Maximum 2 guests.",
                Image = "/images/1bedroom.webp",
                // Total Units updated to 13 (1 on Ground, 2 on Floors 1-6)
                Units = 13, 
                MaxGuests = 2,
                BedSize = "5x6",
                Bedrooms = "1 Bedroom",
                Bathrooms = 1,
                // Ground Floor (Index 0) = 1 unit, Floors 1-6 (Indices 1-6) = 2 units each
                FloorDistribution = new List<int> { 1, 2, 2, 2, 2, 2, 2 },
                Pricing = new ApartmentPricing
                {
                    DailyBedOnly = 120,
                    DailyBB = 140,
                    MonthlyBedOnly = 2800,
                    MonthlyBB = 3200
                },
                Amenities = new List<string>
                {
                    "High-Speed WiFi", "Air Conditioning", "50\" Smart TV",
                    "Full Kitchen", "Coffee Maker", "Refrigerator",
                    "In-room Safe", "Hair Dryer", "Premium Toiletries",
                    "Washer/Dryer", "Work Desk"
                },
                Features = new List<string>
                {
                    "Golf Course View", "5x6 King Size Bed", "Separate Living Area",
                    "Daily Housekeeping", "24/7 Reception", "Pool Access",
                    "Gym Access", "Free Parking"
                }
            },
            new Apartment
            {
                Id = "two-bedroom-apartment",
                Name = "Two Bedroom Apartment",
                Type = "two-bedroom",
                Size = "40 sqm",
                Description = "Luxurious two-bedroom apartment with panoramic views, spacious living area, and premium amenities. The ultimate choice for families or groups seeking comfort and space.",
                LongDescription = "Experience spacious living in our 40sqm Two Bedroom Apartment. This stunning residence offers panoramic views of the golf course, mountains, and surrounding landscape. Two comfortable bedrooms each feature 5x6 beds providing ample space for up to 4 guests, while the open-plan living and dining area is perfect for families or groups. The full kitchen and additional amenities make extended stays comfortable.",
                Image = "/images/2bedroom.webp",
                // Total Units updated to 13 (1 on Ground, 2 on Floors 1-6)
                Units = 13, 
                MaxGuests = 4,
                BedSize = "5x6",
                Bedrooms = "2 Bedrooms",
                Bathrooms = 2,
                // Ground Floor (Index 0) = 1 unit, Floors 1-6 (Indices 1-6) = 2 units each
                FloorDistribution = new List<int> { 1, 2, 2, 2, 2, 2, 2 }, 
                Pricing = new ApartmentPricing
                {
                    DailyBedOnly = 180,
                    DailyBB = 220,
                    MonthlyBedOnly = 4200,
                    MonthlyBB = 4800
                },
                Amenities = new List<string>
                {
                    "High-Speed WiFi", "Air Conditioning", "55\" Smart TV",
                    "Full Kitchen", "Espresso Machine", "Full-size Refrigerator",
                    "In-room Safe", "Premium Bath Amenities", "Washer/Dryer",
                    "Dining Table"
                },
                Features = new List<string>
                {
                    "Panoramic Views", "Two 5x6 Beds", "Spacious Terrace",
                    "Family Friendly", "Daily Housekeeping", "24/7 Reception",
                    "Pool Access", "Gym Access", "Free Parking", "BBQ Access"
                }
            }
        };

        public static List<AmenityPricing> AmenitiesPricing => new()
        {
            new AmenityPricing
            {
                Id = "gym",
                Name = "Gym",
                DailyPrice = 500,
                MonthlyPrice = 5000,
                Description = "State-of-the-art fitness center with modern equipment"
            },
            new AmenityPricing
            {
                Id = "pool",
                Name = "Pool",
                DailyPrice = 500,
                MonthlyPrice = 5000,
                Description = "Infinity pool overlooking the golf course"
            },
            new AmenityPricing
            {
                Id = "steam-bath",
                Name = "Steam Bath",
                DailyPrice = 1000,
                MonthlyPrice = null,
                Description = "Relaxing steam bath for rejuvenation"
            },
            new AmenityPricing
            {
                Id = "sauna",
                Name = "Sauna",
                DailyPrice = 1000,
                MonthlyPrice = null,
                Description = "Traditional dry sauna experience"
            },
            new AmenityPricing
            {
                Id = "gym-pool-combo",
                Name = "Gym + Pool Combined",
                DailyPrice = null,
                MonthlyPrice = 8000,
                Description = "Best value for fitness enthusiasts - save KES 2,000/month"
            }
        };

        public static List<ConferencePackage> ConferencePackages => new()
        {
            new ConferencePackage
            {
                Id = "full-board",
                Name = "Full Board Conference Package",
                PricePerPerson = 4500,
                Description = "Complete conference experience with all meals included",
                Includes = new List<string>
                {
                    "Conference room access",
                    "Morning tea/coffee & snacks",
                    "Full breakfast",
                    "Lunch buffet",
                    "Afternoon tea/coffee & snacks",
                    "Dinner",
                    "Projector & screen",
                    "WiFi access",
                    "Stationery"
                }
            },
            new ConferencePackage
            {
                Id = "half-board",
                Name = "Half Board Conference Package",
                PricePerPerson = 4000,
                Description = "Conference package with breakfast and lunch",
                Includes = new List<string>
                {
                    "Conference room access",
                    "Morning tea/coffee & snacks",
                    "Full breakfast",
                    "Lunch buffet",
                    "Afternoon tea/coffee & snacks",
                    "Projector & screen",
                    "WiFi access",
                    "Stationery"
                }
            }
        };

        public static Apartment? GetApartmentById(string id)
        {
            return Apartments.FirstOrDefault(apt => apt.Id == id);
        }

        public static List<FloorInfo> GetBuildingLayout()
        {
            var random = new Random();
            // TotalFloors is 7 (0=Ground, 1-6=Upper). We iterate from 0 to 6.
            return Enumerable.Range(0, TotalFloors).Select(floorIndex =>
            {
                // Floor number for display/labeling: 0 is Ground, 1 is Floor 1, etc.
                var floorNumber = floorIndex == 0 ? 0 : floorIndex; 
                
                // Get the distribution data for the specific floorIndex
                var studioTotal = Apartments[0].FloorDistribution[floorIndex];
                var oneBedroomTotal = Apartments[1].FloorDistribution[floorIndex];
                var twoBedroomTotal = Apartments[2].FloorDistribution[floorIndex];

                return new FloorInfo
                {
                    // Label the first floor as 0 (Ground)
                    Floor = floorNumber,
                    Studios = new RoomAvailability
                    {
                        Total = studioTotal,
                        // Random availability - slightly higher chance of less being available on upper floors
                        Available = Math.Max(0, studioTotal - random.Next(0, floorIndex == 0 ? 1 : 2)) 
                    },
                    OneBedroom = new RoomAvailability
                    {
                        Total = oneBedroomTotal,
                        Available = Math.Max(0, oneBedroomTotal - random.Next(0, floorIndex == 0 ? 1 : 2))
                    },
                    TwoBedroom = new RoomAvailability
                    {
                        Total = twoBedroomTotal,
                        Available = Math.Max(0, twoBedroomTotal - random.Next(0, floorIndex == 0 ? 1 : 2))
                    }
                };
            }).ToList();
        }
    }
}