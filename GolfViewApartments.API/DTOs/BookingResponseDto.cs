namespace GolfViewApartments.API.DTOs
{
    /// <summary>
    /// DTO for returning booking information with full details
    /// </summary>
    public class BookingResponseDto
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Unique booking reference code (e.g., "A3B2C1D4")
        /// </summary>
        public string BookingReference { get; set; } = string.Empty;

        // ===== GUEST INFORMATION =====
        public int CustomerId { get; set; }
        public string Customer { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // ===== APARTMENT & ROOM INFORMATION =====
        /// <summary>
        /// Apartment ID for reference and linking
        /// </summary>
        public int ApartmentId { get; set; }

        /// <summary>
        /// Apartment type name (e.g., "Studio Apartment", "One Bedroom Apartment")
        /// </summary>
        public string ApartmentName { get; set; } = string.Empty;

        /// <summary>
        /// Apartment type code (e.g., "studio", "one-bedroom", "two-bedroom")
        /// </summary>
        public string ApartmentType { get; set; } = string.Empty;

        /// <summary>
        /// Specific room number assigned to this booking
        /// </summary>
        public string Room { get; set; } = string.Empty;

        /// <summary>
        /// Room type as string (e.g., "Studio", "OneBedroom", "TwoBedroom")
        /// </summary>
        public string RoomType { get; set; } = string.Empty;

        // ===== STAY CONFIGURATION =====
        /// <summary>
        /// Board type selected (e.g., "Bed Only", "Bed & Breakfast")
        /// </summary>
        public string BoardType { get; set; } = string.Empty;

        /// <summary>
        /// Occupancy level (e.g., "Single", "Double", "Quadruple")
        /// </summary>
        public string Occupancy { get; set; } = string.Empty;

        // ===== STAY DATES =====
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        
        /// <summary>
        /// Calculated number of nights
        /// </summary>
        public int TotalNights => (CheckOut - CheckIn).Days;

        // ===== GUEST COUNT =====
        public int Adults { get; set; }
        public int Children { get; set; }
        public string? ChildrenAges { get; set; }
        
        /// <summary>
        /// Total number of guests (adults + children)
        /// </summary>
        public int TotalGuests => Adults + Children;

        // ===== PRICING =====
        /// <summary>
        /// Total price for the entire stay
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Average price per night (calculated)
        /// </summary>
        public decimal PricePerNight => TotalNights > 0 ? TotalPrice / TotalNights : 0;

        // ===== SPECIAL REQUESTS =====
        public string? SpecialRequests { get; set; }

        // ===== BOOKING MANAGEMENT =====
        /// <summary>
        /// Current booking status (e.g., "Pending", "Confirmed", "Cancelled")
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// When the booking was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When the booking was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}