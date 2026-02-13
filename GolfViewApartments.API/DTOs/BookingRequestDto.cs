using System.ComponentModel.DataAnnotations;
using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.API.DTOs
{
    /// <summary>
    /// DTO for creating a new booking request
    /// REVISED to work with existing RoomRate table structure
    /// </summary>
    public class BookingRequestDto
    {
        // ===== CUSTOMER INFORMATION =====
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Guest name is required")]
        [StringLength(100, ErrorMessage = "Guest name cannot exceed 100 characters")]
        public string Customer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string Phone { get; set; } = string.Empty;

        // ===== APARTMENT & ROOM INFORMATION =====
        /// <summary>
        /// Apartment ID for room allocation (optional - will be found by room type if not provided)
        /// </summary>
        public int ApartmentId { get; set; }

        /// <summary>
        /// Specific room number (optional - will be auto-assigned if not provided)
        /// </summary>
        [StringLength(20, ErrorMessage = "Room number cannot exceed 20 characters")]
        public string Room { get; set; } = string.Empty;

        /// <summary>
        /// Room type enum - Must exist in RoomType table
        /// Used to lookup rates in RoomRate table
        /// </summary>
        [Required(ErrorMessage = "Room type is required")]
        public RoomTypeEnum RoomType { get; set; }

        // ===== PRICING CONFIGURATION =====
        /// <summary>
        /// Board type - Must match an entry in RoomRate table
        /// Valid values depend on what's in your database
        /// Typical values: "Bed Only", "Bed & Breakfast", "Half Board", "Full Board"
        /// </summary>
        [Required(ErrorMessage = "Board type is required")]
        [StringLength(50, ErrorMessage = "Board type cannot exceed 50 characters")]
        public string BoardType { get; set; } = string.Empty;

        /// <summary>
        /// Occupancy level - Determines which rate column to use
        /// "Single" → FirstOccupancy
        /// "Double" → SecondOccupancy
        /// "Quadruple" → SecondOccupancy with modifier
        /// </summary>
        [Required(ErrorMessage = "Occupancy is required")]
        [RegularExpression("^(Single|Double|Quadruple)$", 
            ErrorMessage = "Occupancy must be 'Single', 'Double', or 'Quadruple'")]
        public string Occupancy { get; set; } = string.Empty;

        // ===== STAY DATES =====
        [Required(ErrorMessage = "Check-in date is required")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "Check-out date is required")]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        // ===== GUEST COUNT =====
        [Required(ErrorMessage = "Number of adults is required")]
        [Range(1, 10, ErrorMessage = "Number of adults must be between 1 and 10")]
        public int Adults { get; set; }

        [Range(0, 10, ErrorMessage = "Number of children must be between 0 and 10")]
        public int Children { get; set; }

        /// <summary>
        /// JSON array of children ages: "[5, 8, 12]"
        /// </summary>
        public string? ChildrenAges { get; set; }

        // ===== PRICING =====
        /// <summary>
        /// Total price - Will be calculated by server from RoomRate table
        /// Client can provide this for validation, or leave it for server to calculate
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Total price must be greater than 0")]
        public decimal? TotalPrice { get; set; }

        // ===== SPECIAL REQUESTS =====
        [StringLength(1000, ErrorMessage = "Special requests cannot exceed 1000 characters")]
        public string? SpecialRequests { get; set; }

        // ===== COMPUTED PROPERTY =====
        public int TotalGuests => Adults + Children;
    }
}