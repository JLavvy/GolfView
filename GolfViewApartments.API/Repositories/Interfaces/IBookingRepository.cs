using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Repositories.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetByCustomerIdAsync(int customerId);
        Task<IEnumerable<Booking>> GetByStatusAsync(BookingStatus status);
        Task<IEnumerable<Booking>> GetActiveBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsForDateRangeAsync(DateTime checkIn, DateTime checkOut);
        Task<bool> IsRoomBookedForDatesAsync(string roomNumber, DateTime checkIn, DateTime checkOut, int? excludeBookingId = null);
    }
}