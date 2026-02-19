using GolfViewApartments.API.DTOs;
using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.API.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto request);
        Task<BookingResponseDto> GetBookingByIdAsync(int id);
        Task<BookingResponseDto> GetBookingByReferenceAsync(string reference);
        Task<List<BookingResponseDto>> GetAllBookingsAsync();
        Task<List<BookingResponseDto>> GetBookingsByStatusAsync(string status);
        Task<List<BookingResponseDto>> GetBookingsByCustomerAsync(int customerId);
        Task UpdateBookingStatusAsync(int bookingId, BookingStatus status);
        Task CancelBookingAsync(int bookingId);

        Task<List<RoomAvailabilityDto>> GetAvailableRoomsAsync(string type);
        Task<List<RoomAvailabilityDto>> GetAvailableRoomsForDatesAsync(string type, DateTime checkIn, DateTime checkOut);
    }
}
