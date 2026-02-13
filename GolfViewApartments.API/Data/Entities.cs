using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.Data.Entities;

// Customer Entity
public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = "";

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = "";

    [Required]
    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

// Booking Entity
public class Booking
{
    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    public string BookingReference { get; set; } = "";

    [Required]
    public int CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    [Required]
    public int RoomTypeId { get; set; }

    [ForeignKey(nameof(RoomTypeId))]
    public RoomType RoomType { get; set; } = null!;

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set; }

    [Required]
    public int Adults { get; set; }

    public int Children { get; set; } = 0;

    [StringLength(100)]
    public string? ChildrenAges { get; set; }

    [Required]
    [StringLength(50)]
    public string BoardType { get; set; } = "";

    [Required]
    [StringLength(20)]
    public string OccupancyType { get; set; } = "";

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal DailyRate { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalAmount { get; set; }

    [StringLength(1000)]
    public string? SpecialRequests { get; set; }

    [Required]
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}

// Room Type Entity
public class RoomType
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = "";

    [Required]
    public RoomTypeEnum RoomTypeEnum { get; set; }

    [StringLength(50)]
    public string IconClass { get; set; } = "";

    public int MaxOccupancy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<RoomRate> Rates { get; set; } = new List<RoomRate>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

// Room Rate Entity
public class RoomRate
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RoomTypeId { get; set; }

    [ForeignKey(nameof(RoomTypeId))]
    public RoomType RoomType { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string BoardType { get; set; } = "";

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal FirstOccupancy { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal SecondOccupancy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// Conference Package Entity
public class ConferencePackage
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [StringLength(50)]
    public string IconClass { get; set; } = "";

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// Fitness Amenity Entity
public class FitnessAmenity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [StringLength(50)]
    public string IconClass { get; set; } = "";

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal DayRate { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal MonthlyRate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}