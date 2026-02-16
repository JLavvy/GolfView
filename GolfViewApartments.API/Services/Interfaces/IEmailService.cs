using GolfViewApartments.API.DTOs;
using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.API.Services.Interfaces
{
    /// <summary>
    /// Interface for email service operations
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send booking confirmation email to guest
        /// </summary>
        /// <param name="emailData">Booking confirmation email data</param>
        /// <returns>True if email sent successfully, false otherwise</returns>
        Task<bool> SendBookingConfirmationAsync(BookingConfirmationEmail emailData);
    }

    /// <summary>
    /// Data model for booking confirmation email
    /// </summary>
    public class BookingConfirmationEmail
    {
        public string BookingReference { get; set; } = "";
        public string GuestName { get; set; } = "";
        public string GuestEmail { get; set; } = "";
        public string GuestPhone { get; set; } = "";
        public string RoomNumber { get; set; } = "";
        public string RoomType { get; set; } = "";
        public string BoardType { get; set; } = "";
        public string Occupancy { get; set; } = "";
        public string CheckInDate { get; set; } = "";
        public string CheckOutDate { get; set; } = "";
        public int TotalNights { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string? ChildrenAges { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PricePerNight { get; set; }
        public string? SpecialRequests { get; set; }
    }
}