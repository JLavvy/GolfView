namespace GolfViewApartments.API.DTOs
{
    public class AvailabilityRequestDto
    {
        public string ApartmentId { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}