namespace GolfViewApartments.API.DTOs
{
    public class RoomAvailabilityDto
    {
        public string RoomNumber { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Floor { get; set; }
        public bool IsAvailable { get; set; }
    }
}