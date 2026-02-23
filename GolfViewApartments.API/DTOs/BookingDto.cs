namespace GolfViewApartments.API.DTOs
{
    /// <summary>
    /// DTO used for updating an existing booking (admin edit)
    /// </summary>
    public class BookingDto
    {
        public int      Id              { get; set; }
        public string   Customer        { get; set; } = string.Empty;
        public string   Email           { get; set; } = string.Empty;
        public string   Phone           { get; set; } = string.Empty;
        public string   Room            { get; set; } = string.Empty;
        public string   RoomType        { get; set; } = string.Empty;
        public string?  BoardType       { get; set; }
        public string?  Occupancy       { get; set; }
        public DateTime CheckIn         { get; set; }
        public DateTime CheckOut        { get; set; }
        public int      Adults          { get; set; }
        public int      Children        { get; set; }
        public string?  SpecialRequests { get; set; }
        public string?  Status          { get; set; }
    }
}