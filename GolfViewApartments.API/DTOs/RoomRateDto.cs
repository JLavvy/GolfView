namespace GolfViewApartments.API.DTOs
{
    public class RoomRateDto
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public string BoardType { get; set; } = "";
        public decimal FirstOccupancy { get; set; }
        public decimal SecondOccupancy { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}