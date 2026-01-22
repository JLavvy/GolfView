
using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Repositories.Implementations
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Room?> GetByRoomNumberAsync(string roomNumber)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Number == roomNumber);
        }

        public async Task<IEnumerable<Room>> GetByTypeAsync(string type)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(r => r.Type == type)
                .ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetByApartmentIdAsync(int apartmentId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(r => r.ApartmentId == apartmentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(r => r.IsAvailable)
                .ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsForDatesAsync(
            int apartmentId, 
            DateTime checkIn, 
            DateTime checkOut)
        {
            // Get all rooms for this apartment
            var allRooms = await _dbSet
                .AsNoTracking()
                .Where(r => r.ApartmentId == apartmentId)
                .ToListAsync();

            // Get bookings that overlap with the requested dates
            var bookedRoomNumbers = await _context.Bookings
                .AsNoTracking()
                .Where(b => b.ApartmentId == apartmentId
                    && (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Pending)
                    && b.CheckIn < checkOut
                    && b.CheckOut > checkIn)
                .Select(b => b.RoomNumber)
                .ToListAsync();

            // Return rooms that are not booked
            return allRooms.Where(r => !bookedRoomNumbers.Contains(r.Number));
        }
    }
}