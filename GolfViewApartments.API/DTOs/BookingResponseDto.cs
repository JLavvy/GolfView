namespace GolfViewApartments.API.DTOs
{
    public class BookingResponseDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ApartmentName { get; set; } = string.Empty;
        public string RoomNumber { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Nights { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string RentalType { get; set; } = string.Empty;
        public string MealPlan { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? SpecialRequests { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}