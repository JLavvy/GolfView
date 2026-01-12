namespace GolfViewApartments.Models
{
    public class RoomRate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DailyBedOnly { get; set; } = string.Empty;
        public string DailyBnB { get; set; } = string.Empty;
        public string MonthlyBedOnly { get; set; } = string.Empty;
        public string MonthlyBnB { get; set; } = string.Empty;
    }
}