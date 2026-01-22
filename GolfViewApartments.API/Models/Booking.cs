using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfViewApartments.API.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int ApartmentId { get; set; }

        [Required]
        [StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty;

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public int Adults { get; set; }
        public int Children { get; set; }

        public string ChildrenAges { get; set; } = "[]";

        [Required]
        [StringLength(50)]
        public string RentalType { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string MealPlan { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; }

        [StringLength(1000)]
        public string? SpecialRequests { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Customer Customer { get; set; } = null!;
        public Apartment Apartment { get; set; } = null!;
    }

    // ============================================
    // BOOKING STATUS ENUM
    // ============================================
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }
}