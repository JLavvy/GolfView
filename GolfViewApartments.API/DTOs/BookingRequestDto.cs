using System.ComponentModel.DataAnnotations;

namespace GolfViewApartments.API.DTOs
{
    public class BookingRequestDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Phone { get; set; } = string.Empty;
        
        [Required]
        public int ApartmentId { get; set; }
        
        [Required]
        public string RentalType { get; set; } = string.Empty;
        
        [Required]
        public string MealPlan { get; set; } = string.Empty;
        
        [Required]
        public DateTime CheckIn { get; set; }
        
        [Required]
        public DateTime CheckOut { get; set; }
        
        [Required]
        [Range(1, 10)]
        public int Adults { get; set; }
        
        [Range(0, 10)]
        public int Children { get; set; }
        
        public int[] ChildrenAges { get; set; } = Array.Empty<int>();
        
        public string? SpecialRequests { get; set; }
    }
}