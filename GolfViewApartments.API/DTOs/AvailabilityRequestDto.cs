namespace GolfViewApartments.API.DTOs
{
    public class AvailabilityRequestDto
    {
        public int ApartmentId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}