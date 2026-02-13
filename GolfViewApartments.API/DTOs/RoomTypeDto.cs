using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.API.DTOs
{
    public class RoomTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public RoomTypeEnum RoomTypeEnum { get; set; }
        public string IconClass { get; set; } = "";
        public int MaxOccupancy { get; set; }

        public List<RoomRateDto> Rates { get; set; } = new();
        public DateTime CreatedAt { get; set; }

    }
}