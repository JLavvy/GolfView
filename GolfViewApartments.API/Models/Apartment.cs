
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfViewApartments.API.Models
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ApartmentId { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [StringLength(50)]
        public string Size { get; set; } = string.Empty;

        public int MaxGuests { get; set; }
        public int TotalUnits { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DailyBedOnly { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DailyBB { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyBedOnly { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyBB { get; set; }

        // Navigation properties
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}