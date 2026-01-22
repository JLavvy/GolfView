using GolfViewApartments.Models;

namespace GolfViewApartments.Data
{
    public static class AmenitiesData
    {
        // Core Amenities List (for quick display/filtering)
        public static List<string> Amenities { get; } = new()
        {
            "Infinity Pool & Jacuzzi",
            "Fitness Center",
            "Steam Bath & Sauna",
            "Golf Course Access",
            "Conference Facilities",
            "Ample Parking"
        };

        // Meal Plan Information
        public static Dictionary<string, MealPlanInfo> MealPlans { get; } = new()
        {
            ["bed-only"] = new MealPlanInfo
            {
                Label = "Bed Only",
                ShortLabel = "B/O",
                Description = "Room accommodation without meals"
            },
            ["bed-breakfast"] = new MealPlanInfo
            {
                Label = "Bed & Breakfast",
                ShortLabel = "B&B",
                Description = "Room accommodation with daily breakfast"
            }
        };

        // Rental Type Information
        public static Dictionary<string, RentalTypeInfo> RentalTypes { get; } = new()
        {
            ["daily"] = new RentalTypeInfo
            {
                Label = "Daily Rate",
                Description = "Perfect for short stays and weekend getaways"
            },
            ["monthly"] = new RentalTypeInfo
            {
                Label = "Monthly Rate",
                Description = "Extended stay with discounted rates"
            }
        };

        // Detailed Amenity Features (for amenities page)
        public static List<Feature> GetDetailedAmenities()
        {
            return new List<Feature>
            {
                new Feature
                {
                    Icon = "lucide-waves",
                    Title = "Infinity Pool",
                    Description = "Relax in our stunning infinity pool overlooking the golf course. Open daily from 7 AM to 10 PM."
                },
                new Feature
                {
                    Icon = "lucide-droplets",
                    Title = "Jacuzzi",
                    Description = "Unwind in our heated jacuzzi with therapeutic jets and scenic views."
                },
                new Feature
                {
                    Icon = "lucide-dumbbell",
                    Title = "Fitness Center",
                    Description = "State-of-the-art gym equipment, personal training available. Open 24/7 for residents."
                },
                new Feature
                {
                    Icon = "lucide-flame",
                    Title = "Steam Bath",
                    Description = "Relaxing steam bath for rejuvenation after your workout or a long day."
                },
                new Feature
                {
                    Icon = "lucide-sparkles",
                    Title = "Sauna",
                    Description = "Traditional dry sauna experience for ultimate relaxation."
                },
                new Feature
                {
                    Icon = "lucide-utensils-crossed",
                    Title = "On-site Restaurant",
                    Description = "Fine dining experience with locally-sourced ingredients. Breakfast, lunch, and dinner served daily with room service available."
                },
                new Feature
                {
                    Icon = "lucide-circle-dot",
                    Title = "Golf Course Access",
                    Description = "Preferred tee times and special rates at the championship 18-hole golf course. Equipment rental and lessons available."
                },
                new Feature
                {
                    Icon = "lucide-presentation",
                    Title = "Conference Facilities",
                    Description = "Modern conference rooms with full AV equipment, perfect for business meetings and events."
                },
                new Feature
                {
                    Icon = "lucide-bell",
                    Title = "24/7 Concierge",
                    Description = "Our dedicated concierge team is available around the clock to assist with reservations, tours, and special requests."
                },
                new Feature
                {
                    Icon = "lucide-car",
                    Title = "Ample Parking",
                    Description = "Complimentary parking for all guests. Electric vehicle charging stations available."
                },
                new Feature
                {
                    Icon = "lucide-wifi",
                    Title = "High-Speed WiFi",
                    Description = "Complimentary high-speed internet throughout the property, including pool area and outdoor spaces."
                },
                new Feature
                {
                    Icon = "lucide-shield-check",
                    Title = "24/7 Security",
                    Description = "Round-the-clock security with CCTV monitoring, secure key card access, and on-site security personnel."
                },
                new Feature
                {
                    Icon = "lucide-shirt",
                    Title = "Laundry Service",
                    Description = "Same-day laundry and dry cleaning services available. In-unit washer/dryer in select apartments."
                }
            };
        }

        // Apartment Features (for apartment listings)
        public static List<Feature> GetApartmentFeatures()
        {
            return new List<Feature>
            {
                new Feature { Icon = "lucide-wifi", Title = "High-Speed WiFi", Description = "Complimentary internet" },
                new Feature { Icon = "lucide-tv", Title = "Smart TV", Description = "Streaming enabled" },
                new Feature { Icon = "lucide-wind", Title = "Air Conditioning", Description = "Climate control" },
                new Feature { Icon = "lucide-washing-machine", Title = "Washer/Dryer", Description = "In-unit laundry" },
                new Feature { Icon = "lucide-chef-hat", Title = "Full Kitchen", Description = "Fully equipped" },
                new Feature { Icon = "lucide-shield-check", Title = "24/7 Security", Description = "Safe & secure" },
                new Feature { Icon = "lucide-car", Title = "Free Parking", Description = "Dedicated space" },
                new Feature { Icon = "lucide-dumbbell", Title = "Gym Access", Description = "Fitness center" }
            };
        }

        // All Apartments
        public static List<Apartment> GetAllApartments()
        {
            return new List<Apartment>
            {
                new Apartment
                {
                    Id = "studio",
                    Name = "Studio Apartment",
                    Type = "studio",
                    Description = "Cozy and efficient space perfect for solo travelers or couples",
                    LongDescription = "Our studio apartments feature an open-plan design that maximizes space and comfort. Perfect for business travelers or couples seeking a romantic getaway, each unit comes fully furnished with modern amenities and stunning golf course views.",
                    Pricing = new ApartmentPricing
                    {
                        DailyBedOnly = 3500m,
                        DailyBB = 4500m,
                        MonthlyBedOnly = 35000m,
                        MonthlyBB = 45000m
                    },
                    Image = "/assets/studio.jpg",
                    MaxGuests = 2,
                    Size = "35 sqm",
                    BedSize = "Queen bed",
                    Bedrooms = "Studio",
                    Bathrooms = 1,
                    Units = 10,
                    FloorDistribution = new List<int> { 2, 2, 2, 2, 2 },
                    Amenities = new List<string>
                    {
                        "Air conditioning",
                        "High-speed WiFi",
                        "Smart TV",
                        "Kitchenette",
                        "Private bathroom",
                        "Work desk",
                        "Golf course view",
                        "Daily housekeeping"
                    },
                    Features = new List<string>
                    {
                        "Queen-size bed",
                        "Fully equipped kitchenette",
                        "Modern bathroom with shower",
                        "Work desk and ergonomic chair",
                        "Ample storage space",
                        "Balcony with golf course view"
                    }
                },
                new Apartment
                {
                    Id = "one-bedroom",
                    Name = "One Bedroom Apartment",
                    Type = "one-bedroom",
                    Description = "Spacious apartment with separate bedroom and living area",
                    LongDescription = "Experience comfort and privacy in our one-bedroom apartments. With a separate bedroom and spacious living area, these units are ideal for extended stays or small families. Enjoy the fully equipped kitchen and panoramic views of the championship golf course.",
                    Pricing = new ApartmentPricing
                    {
                        DailyBedOnly = 5000m,
                        DailyBB = 6500m,
                        MonthlyBedOnly = 50000m,
                        MonthlyBB = 65000m
                    },
                    Image = "/assets/one-bedroom.jpg",
                    MaxGuests = 3,
                    Size = "55 sqm",
                    BedSize = "King bed",
                    Bedrooms = "1",
                    Bathrooms = 1,
                    Units = 15,
                    FloorDistribution = new List<int> { 3, 3, 3, 3, 3 },
                    Amenities = new List<string>
                    {
                        "Air conditioning",
                        "High-speed WiFi",
                        "Smart TV",
                        "Full kitchen",
                        "Private bathroom",
                        "Dining area",
                        "Living room",
                        "Golf course view",
                        "Daily housekeeping",
                        "In-unit washer/dryer"
                    },
                    Features = new List<string>
                    {
                        "King-size bed in bedroom",
                        "Separate living room with sofa bed",
                        "Full kitchen with refrigerator, stove, microwave",
                        "Dining table for 4",
                        "Modern bathroom with bathtub",
                        "Large balcony with seating area",
                        "Walk-in closet"
                    }
                },
                new Apartment
                {
                    Id = "two-bedroom",
                    Name = "Two Bedroom Apartment",
                    Type = "two-bedroom",
                    Description = "Luxury suite perfect for families or groups",
                    LongDescription = "Our premium two-bedroom apartments offer the ultimate in space and luxury. Perfect for families or groups, these spacious units feature two separate bedrooms, a large living area, and a fully equipped kitchen. Experience resort-style living with breathtaking views and top-tier amenities.",
                    Pricing = new ApartmentPricing
                    {
                        DailyBedOnly = 7500m,
                        DailyBB = 9500m,
                        MonthlyBedOnly = 75000m,
                        MonthlyBB = 95000m
                    },
                    Image = "/assets/two-bedroom.jpg",
                    MaxGuests = 5,
                    Size = "85 sqm",
                    BedSize = "King + Queen beds",
                    Bedrooms = "2",
                    Bathrooms = 2,
                    Units = 10,
                    FloorDistribution = new List<int> { 2, 2, 2, 2, 2 },
                    Amenities = new List<string>
                    {
                        "Air conditioning",
                        "High-speed WiFi",
                        "Smart TVs in all rooms",
                        "Full kitchen",
                        "2 private bathrooms",
                        "Dining area",
                        "Living room",
                        "Golf course view",
                        "Daily housekeeping",
                        "In-unit washer/dryer",
                        "Premium linens",
                        "Welcome basket"
                    },
                    Features = new List<string>
                    {
                        "Master bedroom with king bed and ensuite",
                        "Second bedroom with queen bed",
                        "Spacious living room with sectional sofa",
                        "Gourmet kitchen with full appliances",
                        "Dining table for 6",
                        "Two modern bathrooms",
                        "Expansive balcony with outdoor furniture",
                        "Multiple closets and storage",
                        "Premium finishes throughout"
                    }
                }
            };
        }

        // Amenity Pricing
        public static List<AmenityPricing> GetAmenityPricing()
        {
            return new List<AmenityPricing>
            {
                new AmenityPricing
                {
                    Id = "pool",
                    Name = "Infinity Pool & Jacuzzi",
                    DailyPrice = 500m,
                    MonthlyPrice = 5000m,
                    Description = "Access to our stunning infinity pool and heated jacuzzi overlooking the golf course. Open daily 7 AM - 10 PM."
                },
                new AmenityPricing
                {
                    Id = "gym",
                    Name = "Fitness Center",
                    DailyPrice = 500m,
                    MonthlyPrice = 5000m,
                    Description = "24/7 access to state-of-the-art gym equipment. Personal training available at additional cost."
                },
                new AmenityPricing
                {
                    Id = "steam",
                    Name = "Steam Bath",
                    DailyPrice = 1000m,
                    MonthlyPrice = null,
                    Description = "Relaxing steam bath experience. Daily access per session."
                },
                new AmenityPricing
                {
                    Id = "sauna",
                    Name = "Sauna",
                    DailyPrice = 1000m,
                    MonthlyPrice = null,
                    Description = "Traditional dry sauna. Daily access per session."
                }
            };
        }

        // Conference Packages
        public static List<ConferencePackage> GetConferencePackages()
        {
            return new List<ConferencePackage>
            {
                new ConferencePackage
                {
                    Id = "half-board",
                    Name = "Half-Board Package",
                    PricePerPerson = 4000m,
                    Description = "Perfect for day conferences and workshops",
                    Includes = new List<string>
                    {
                        "Conference room rental",
                        "Morning tea/coffee with snacks",
                        "Lunch buffet",
                        "Afternoon tea/coffee",
                        "Standard AV equipment",
                        "WiFi access",
                        "Notepads and pens",
                        "Bottled water"
                    }
                },
                new ConferencePackage
                {
                    Id = "full-board",
                    Name = "Full-Board Package",
                    PricePerPerson = 7500m,
                    Description = "Comprehensive package for multi-day events",
                    Includes = new List<string>
                    {
                        "Conference room rental",
                        "Breakfast buffet",
                        "Morning tea/coffee with snacks",
                        "Lunch buffet",
                        "Afternoon tea/coffee with snacks",
                        "Dinner (3-course meal)",
                        "Premium AV equipment",
                        "WiFi access",
                        "Notepads, pens, and folders",
                        "Bottled water and soft drinks",
                        "Dedicated event coordinator"
                    }
                },
            };
        }

        // Floor Information
        public static List<FloorInfo> GetFloorAvailability()
        {
            return new List<FloorInfo>
            {
                new FloorInfo
                {
                    Floor = 1,
                    Studios = new RoomAvailability { Total = 2, Available = 1 },
                    OneBedroom = new RoomAvailability { Total = 3, Available = 2 },
                    TwoBedroom = new RoomAvailability { Total = 2, Available = 1 }
                },
                new FloorInfo
                {
                    Floor = 2,
                    Studios = new RoomAvailability { Total = 2, Available = 2 },
                    OneBedroom = new RoomAvailability { Total = 3, Available = 1 },
                    TwoBedroom = new RoomAvailability { Total = 2, Available = 2 }
                },
                new FloorInfo
                {
                    Floor = 3,
                    Studios = new RoomAvailability { Total = 2, Available = 0 },
                    OneBedroom = new RoomAvailability { Total = 3, Available = 3 },
                    TwoBedroom = new RoomAvailability { Total = 2, Available = 1 }
                },
                new FloorInfo
                {
                    Floor = 4,
                    Studios = new RoomAvailability { Total = 2, Available = 1 },
                    OneBedroom = new RoomAvailability { Total = 3, Available = 2 },
                    TwoBedroom = new RoomAvailability { Total = 2, Available = 2 }
                },
                new FloorInfo
                {
                    Floor = 5,
                    Studios = new RoomAvailability { Total = 2, Available = 2 },
                    OneBedroom = new RoomAvailability { Total = 3, Available = 1 },
                    TwoBedroom = new RoomAvailability { Total = 2, Available = 0 }
                }
            };
        }

        // Helper Methods
        public static Apartment? GetApartmentById(string id)
        {
            return GetAllApartments().FirstOrDefault(a => a.Id == id);
        }

        public static List<Apartment> GetApartmentsByType(string type)
        {
            return GetAllApartments().Where(a => a.Type == type).ToList();
        }

        public static decimal GetApartmentPrice(string apartmentId, string rentalType, string mealPlan)
        {
            var apartment = GetApartmentById(apartmentId);
            if (apartment == null) return 0;

            return (rentalType, mealPlan) switch
            {
                ("daily", "bed-only") => apartment.Pricing.DailyBedOnly,
                ("daily", "bed-breakfast") => apartment.Pricing.DailyBB,
                ("monthly", "bed-only") => apartment.Pricing.MonthlyBedOnly,
                ("monthly", "bed-breakfast") => apartment.Pricing.MonthlyBB,
                _ => 0
            };
        }

        public static int GetTotalAvailableRooms(string apartmentType)
        {
            var floors = GetFloorAvailability();
            return apartmentType switch
            {
                "studio" => floors.Sum(f => f.Studios.Available),
                "one-bedroom" => floors.Sum(f => f.OneBedroom.Available),
                "two-bedroom" => floors.Sum(f => f.TwoBedroom.Available),
                _ => 0
            };
        }

        public static int GetTotalRooms(string apartmentType)
        {
            var floors = GetFloorAvailability();
            return apartmentType switch
            {
                "studio" => floors.Sum(f => f.Studios.Total),
                "one-bedroom" => floors.Sum(f => f.OneBedroom.Total),
                "two-bedroom" => floors.Sum(f => f.TwoBedroom.Total),
                _ => 0
            };
        }

        public static bool IsAmenityIncluded(string amenityName)
        {
            return Amenities.Any(a => a.Contains(amenityName, StringComparison.OrdinalIgnoreCase));
        }
    }
}