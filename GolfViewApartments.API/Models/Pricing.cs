using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GolfViewApartments.Shared.Enums;


namespace GolfViewApartments.API.Models{

    
    // Existing Room Type Entity
    public class RoomType
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";
        public int MaxOccupancy { get; set; }

        
        [Required]
        public RoomTypeEnum RoomTypeEnum { get; set; }
        
        [MaxLength(50)]
        public string IconClass { get; set; } = "";
        
        // Navigation property
        public ICollection<RoomRate> Rates { get; set; } = new List<RoomRate>();
        public DateTime CreatedAt { get; set; }

    }

    // Existing Room Rate Entity
    public class RoomRate
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int RoomTypeId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string BoardType { get; set; } = "";
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FirstOccupancy { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SecondOccupancy { get; set; }
        
        // Navigation property
        [ForeignKey("RoomTypeId")]
        public RoomType? RoomType { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    // NEW: Conference Package Entity
    public class ConferencePackage
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";
        
        [MaxLength(50)]
        public string IconClass { get; set; } = "";
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

    // NEW: Fitness Amenity Entity
    public class FitnessAmenity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";
        
        [MaxLength(50)]
        public string IconClass { get; set; } = "";
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DayRate { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyRate { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }


}