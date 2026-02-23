
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfViewApartments.API.Models
{
    public class ContactInfo
    {
    [Key]
        public int Id { get; set; }

        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string WhatsApp { get; set; } = string.Empty;

        [StringLength(200)]
        public string Website { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [StringLength(200)]
        public string FacebookUrl { get; set; } = string.Empty;

        [StringLength(200)]
        public string InstagramUrl { get; set; } = string.Empty;

        [StringLength(200)]
        public string TwitterUrl { get; set; } = string.Empty;
        public string MondayFridayHours { get; set; } = "";
    public string SaturdayHours { get; set; } = "";
    public string SundayHours { get; set; } = "";

        public DateTime UpdatedAt { get; set; }
    }
}