namespace GolfViewApartments.Models
{
    // Meal Plan Info
    public class MealPlanInfo
    {
        public string Label { get; set; } = string.Empty;
        public string ShortLabel { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    // Rental Type Info
    public class RentalTypeInfo
    {
        public string Label { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    // Feature
    public class Feature
    {
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    // Apartment Pricing
    public class ApartmentPricing
    {
        public decimal DailyBedOnly { get; set; }
        public decimal DailyBB { get; set; }
        public decimal MonthlyBedOnly { get; set; }
        public decimal MonthlyBB { get; set; }
    }

    // Apartment
    public class Apartment
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public ApartmentPricing Pricing { get; set; } = new();
        public string Image { get; set; } = string.Empty;
        public int MaxGuests { get; set; }
        public string Size { get; set; } = string.Empty;
        public string BedSize { get; set; } = string.Empty;
        public string Bedrooms { get; set; } = string.Empty;
        public int Bathrooms { get; set; }
        public int Units { get; set; }
        public List<int> FloorDistribution { get; set; } = new();
        public List<string> Amenities { get; set; } = new();
        public List<string> Features { get; set; } = new();
    }

    // Amenity Pricing
    public class AmenityPricing
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal? DailyPrice { get; set; }
        public decimal? MonthlyPrice { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    // Conference Package
    public class ConferencePackage
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal PricePerPerson { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<string> Includes { get; set; } = new();
    }

    // Room Availability
    public class RoomAvailability
    {
        public int Total { get; set; }
        public int Available { get; set; }
    }

    // Floor Info
    public class FloorInfo
    {
        public int Floor { get; set; }
        public RoomAvailability Studios { get; set; } = new();
        public RoomAvailability OneBedroom { get; set; } = new();
        public RoomAvailability TwoBedroom { get; set; } = new();
    }
}