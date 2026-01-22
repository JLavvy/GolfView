using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfViewApartments.API.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Url { get; set; } = string.Empty;

        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
        public DateTime UploadedAt { get; set; }
}
}