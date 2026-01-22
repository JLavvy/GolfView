namespace GolfViewApartments.Models
{
    public class AdBooking
    {
        public int Id { get; set; }
        public string Guest { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public string CheckIn { get; set; } = string.Empty;
        public string CheckOut { get; set; } = string.Empty;
        public int Adults { get; set; }
        public int Children { get; set; }
        public BookingStatus Status { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }
}