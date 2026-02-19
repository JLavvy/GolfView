using GolfViewApartments.API.DTOs;

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
        public int AvailableRooms { get; set; }

        /// <summary>
        /// All board type rates for this apartment's room type.
        /// Sourced from RoomRate table via RoomType → RoomRate.
        /// </summary>
        public List<RoomRateDto> Rates { get; set; } = new();
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

        /// <summary>
        /// Lowest single occupancy / Bed Only rate for this apartment type.
        /// Sourced from RoomRate table, not the Apartment table.
        /// </summary>
        public decimal StartingPrice { get; set; }
    }
}