using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Repositories.Implementations
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Booking>> GetByCustomerIdAsync(int customerId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .Where(b => b.CustomerId == customerId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByStatusAsync(BookingStatus status)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .Where(b => b.Status == status)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetActiveBookingsAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .Where(b => b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Pending)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsForDateRangeAsync(DateTime checkIn, DateTime checkOut)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .Where(b => b.CheckIn < checkOut && b.CheckOut > checkIn
                            && (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Pending))
                .ToListAsync();
        }

        public async Task<bool> IsRoomBookedForDatesAsync(
            string roomNumber, 
            DateTime checkIn, 
            DateTime checkOut, 
            int? excludeBookingId = null)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(b => b.RoomNumber == roomNumber
                            && (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Pending)
                            && b.CheckIn < checkOut 
                            && b.CheckOut > checkIn);

            if (excludeBookingId.HasValue)
            {
                query = query.Where(b => b.Id != excludeBookingId.Value);
            }

            return await query.AnyAsync();
        }
    }
}