using System.ComponentModel.DataAnnotations;

namespace GolfViewApartments.API.DTOs.Apartment
{
    // ============================================
    // APARTMENT RESPONSE DTO
    // ============================================
    public class ApartmentResponseDto
    {
        public int Id { get; set; }
        public string ApartmentId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public int MaxGuests { get; set; }
        public int TotalUnits { get; set; }
        public decimal DailyBedOnly { get; set; }
        public decimal DailyBB { get; set; }
        public decimal MonthlyBedOnly { get; set; }
        public decimal MonthlyBB { get; set; }
        public int AvailableRooms { get; set; }
    }

    // ============================================
    // APARTMENT SUMMARY DTO
    // ============================================
    public class ApartmentSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int AvailableRooms { get; set; }
        public decimal StartingPrice { get; set; }
    }

    // ============================================
    // UPDATE APARTMENT PRICING DTO
    // ============================================
    public class UpdateApartmentPricingDto
    {
        [Required]
        [Range(0, 999999)]
        public decimal DailyBedOnly { get; set; }

        [Required]
        [Range(0, 999999)]
        public decimal DailyBB { get; set; }

        [Required]
        [Range(0, 999999)]
        public decimal MonthlyBedOnly { get; set; }

        [Required]
        [Range(0, 999999)]
        public decimal MonthlyBB { get; set; }
    }
}