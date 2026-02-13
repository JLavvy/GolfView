using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.API.Models
{
    /// <summary>
    /// Booking entity - REVISED to work with existing RoomRate table
    /// </summary>
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string BookingReference { get; set; } = string.Empty;

        // ===== CUSTOMER INFORMATION =====
        [Required]
        public int CustomerId { get; set; }

        // ===== APARTMENT & ROOM INFORMATION =====
        /// <summary>
        /// Foreign key to Apartments table
        /// Used for room allocation and inventory management
        /// </summary>
        [Required]
        public int ApartmentId { get; set; }

        /// <summary>
        /// Specific room number (e.g., "101", "201", "301")
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Room { get; set; } = string.Empty;

        /// <summary>
        /// Room type enum - Links to RoomType table via RoomTypeEnum
        /// Used for pricing lookup in RoomRate table
        /// Must match Apartment.Type
        /// </summary>
        [Required]
        public RoomTypeEnum RoomType { get; set; }

        // ===== PRICING CONFIGURATION =====
        /// <summary>
        /// Board type - Links to RoomRate.BoardType
        /// Examples: "Bed Only", "Bed & Breakfast", "Half Board", "Full Board"
        /// Must exist in RoomRate table for the selected RoomType
        /// </summary>
        [Required]
        [StringLength(50)]
        public string BoardType { get; set; } = string.Empty;

        /// <summary>
        /// Occupancy level - Used to determine which rate to use from RoomRate table
        /// "Single" → Use FirstOccupancy rate
        /// "Double" → Use SecondOccupancy rate
        /// "Quadruple" → Use SecondOccupancy rate with modifier
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Occupancy { get; set; } = string.Empty;

        // ===== DATES =====
        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        // ===== GUEST COUNT =====
        [Required]
        [Range(1, 10)]
        public int Adults { get; set; }

        [Range(0, 10)]
        public int Children { get; set; }

        /// <summary>
        /// JSON array of children ages: "[5, 8, 12]"
        /// </summary>
        public string? ChildrenAges { get; set; }

        // ===== PRICING =====
        /// <summary>
        /// Total calculated price based on:
        /// - Rate from RoomRate table (FirstOccupancy or SecondOccupancy)
        /// - Number of nights
        /// - Optional discounts
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        // ===== SPECIAL REQUESTS =====
        [StringLength(1000)]
        public string? SpecialRequests { get; set; }

        // ===== BOOKING MANAGEMENT =====
        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // ===== NAVIGATION PROPERTIES =====
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        [ForeignKey(nameof(ApartmentId))]
        public Apartment Apartment { get; set; } = null!;

        // ===== COMPUTED PROPERTIES =====
        [NotMapped]
        public int TotalNights => (CheckOut - CheckIn).Days;

        [NotMapped]
        public int TotalGuests => Adults + Children;
    }
}