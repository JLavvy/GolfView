using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfViewApartments.API.Models
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// URL-friendly identifier e.g. "studio-apartment", "one-bedroom-apartment"
        /// Used by frontend to match apartments (ApartmentId == RoomType lookup key)
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ApartmentId { get; set; } = string.Empty;

        /// <summary>
        /// Display name e.g. "Studio Apartment"
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type code used for DB lookups: "studio", "one-bedroom", "two-bedroom"
        /// Must match the switch cases in BookingService and AvailabilityService
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Display size e.g. "24 sqm"
        /// </summary>
        [StringLength(50)]
        public string Size { get; set; } = string.Empty;

        /// <summary>
        /// Maximum guests this apartment type supports
        /// Used in availability filtering
        /// </summary>
        public int MaxGuests { get; set; }

        /// <summary>
        /// Total number of physical units of this type in the building
        /// </summary>
        public int TotalUnits { get; set; }

        // Navigation properties
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}