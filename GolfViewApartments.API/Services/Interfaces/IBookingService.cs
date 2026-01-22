using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto request);

        Task<BookingResponseDto> GetBookingByIdAsync(int id);

        Task<List<BookingResponseDto>> GetAllBookingsAsync();

        Task<List<BookingResponseDto>> GetBookingsByStatusAsync(string status);

        Task UpdateBookingStatusAsync(int bookingId, BookingStatus status);

        Task CancelBookingAsync(int bookingId);

        Task<List<RoomAvailabilityDto>> GetAvailableRoomsAsync(string apartmentType);
    }
}
