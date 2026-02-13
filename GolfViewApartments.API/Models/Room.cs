using System.ComponentModel.DataAnnotations;

namespace GolfViewApartments.API.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Number { get; set; } = string.Empty;

        public int ApartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; } = string.Empty;

        public int Floor { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Navigation property
        public Apartment Apartment { get; set; } = null!;
    }
}