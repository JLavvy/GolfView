using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using GolfViewApartments.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Repositories.Implementations
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        // ===== OVERRIDE BASE METHODS =====

        public override async Task<Booking?> GetByIdAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public override async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        // ===== CUSTOMER QUERIES =====

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

        // ===== STATUS QUERIES =====

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
                .Where(b => b.Status == BookingStatus.Confirmed || 
                           b.Status == BookingStatus.Pending ||
                           b.Status == BookingStatus.CheckedIn)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        // ===== APARTMENT QUERIES =====

        public async Task<IEnumerable<Booking>> GetByApartmentIdAsync(int apartmentId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .Where(b => b.ApartmentId == apartmentId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByApartmentAndDateRangeAsync(
            int apartmentId,
            DateTime checkIn,
            DateTime checkOut)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .Where(b => b.ApartmentId == apartmentId
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.Pending ||
                                b.Status == BookingStatus.CheckedIn)
                            && b.CheckIn < checkOut
                            && b.CheckOut > checkIn)
                .OrderBy(b => b.CheckIn)
                .ToListAsync();
        }

        // ===== ROOM TYPE QUERIES =====

        public async Task<IEnumerable<Booking>> GetBookingsByRoomTypeAsync(
            RoomTypeEnum roomType, 
            DateTime checkIn, 
            DateTime checkOut)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .Where(b => b.RoomType == roomType
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.Pending ||
                                b.Status == BookingStatus.CheckedIn)
                            && b.CheckIn < checkOut
                            && b.CheckOut > checkIn)
                .OrderBy(b => b.CheckIn)
                .ToListAsync();
        }

        public async Task<int> GetBookingCountByRoomTypeAsync(
            RoomTypeEnum roomType, 
            DateTime checkIn, 
            DateTime checkOut)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(b => b.RoomType == roomType
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.Pending ||
                                b.Status == BookingStatus.CheckedIn)
                            && b.CheckIn < checkOut
                            && b.CheckOut > checkIn)
                .CountAsync();
        }

        // ===== ROOM QUERIES =====

        public async Task<IEnumerable<Booking>> GetBookingsByRoomAsync(
            string roomNumber,
            DateTime checkIn,
            DateTime checkOut)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .Where(b => b.Room == roomNumber
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.Pending ||
                                b.Status == BookingStatus.CheckedIn)
                            && b.CheckIn < checkOut
                            && b.CheckOut > checkIn)
                .OrderBy(b => b.CheckIn)
                .ToListAsync();
        }

        // ===== DATE RANGE QUERIES =====

        public async Task<IEnumerable<Booking>> GetBookingsForDateRangeAsync(
            DateTime checkIn, 
            DateTime checkOut)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .Where(b => b.CheckIn < checkOut && b.CheckOut > checkIn
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.Pending ||
                                b.Status == BookingStatus.CheckedIn))
                .OrderBy(b => b.CheckIn)
                .ToListAsync();
        }

        // ===== AVAILABILITY CHECKS =====

        public async Task<bool> IsRoomAvailableForDatesAsync(
            string roomNumber,
            DateTime checkIn,
            DateTime checkOut,
            int? excludeBookingId = null)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(b => b.Room == roomNumber
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.Pending ||
                                b.Status == BookingStatus.CheckedIn)
                            && b.CheckIn < checkOut
                            && b.CheckOut > checkIn);

            if (excludeBookingId.HasValue)
            {
                query = query.Where(b => b.Id != excludeBookingId.Value);
            }

            var hasConflict = await query.AnyAsync();
            return !hasConflict;
        }

        public async Task<bool> IsRoomTypeAvailableForDatesAsync(
            RoomTypeEnum roomType,
            DateTime checkIn,
            DateTime checkOut,
            int? excludeBookingId = null)
        {
            // Get total units for this room type (all are 13 according to seeded data)
            var totalUnits = 13;

            // Count bookings for this room type in the date range
            var query = _dbSet
                .AsNoTracking()
                .Where(b => b.RoomType == roomType
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.Pending ||
                                b.Status == BookingStatus.CheckedIn)
                            && b.CheckIn < checkOut
                            && b.CheckOut > checkIn);

            if (excludeBookingId.HasValue)
            {
                query = query.Where(b => b.Id != excludeBookingId.Value);
            }

            var bookingCount = await query.CountAsync();
            return bookingCount < totalUnits;
        }

        public async Task<bool> IsApartmentAvailableForDatesAsync(
            int apartmentId,
            DateTime checkIn,
            DateTime checkOut,
            int? excludeBookingId = null)
        {
            // Get apartment to find total units
            var apartment = await _context.Apartments.FindAsync(apartmentId);
            if (apartment == null)
                return false;

            var totalUnits = apartment.TotalUnits;

            // Count bookings for this apartment in the date range
            var query = _dbSet
                .AsNoTracking()
                .Where(b => b.ApartmentId == apartmentId
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.Pending ||
                                b.Status == BookingStatus.CheckedIn)
                            && b.CheckIn < checkOut
                            && b.CheckOut > checkIn);

            if (excludeBookingId.HasValue)
            {
                query = query.Where(b => b.Id != excludeBookingId.Value);
            }

            var bookingCount = await query.CountAsync();
            return bookingCount < totalUnits;
        }

        public async Task<List<string>> GetAvailableRoomsAsync(
            RoomTypeEnum roomType,
            DateTime checkIn,
            DateTime checkOut)
        {
            // Get all room numbers for this type from seeded data
            var allRooms = GetAllRoomNumbersForType(roomType);

            // Get booked rooms for the date range
            var bookedRooms = await _dbSet
                .AsNoTracking()
                .Where(b => b.RoomType == roomType
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.Pending ||
                                b.Status == BookingStatus.CheckedIn)
                            && b.CheckIn < checkOut
                            && b.CheckOut > checkIn)
                .Select(b => b.Room)
                .Distinct()
                .ToListAsync();

            // Return available rooms
            return allRooms.Except(bookedRooms).ToList();
        }

        // ===== BOOKING REFERENCE QUERIES =====

        public async Task<Booking?> GetByBookingReferenceAsync(string bookingReference)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .FirstOrDefaultAsync(b => b.BookingReference == bookingReference);
        }

        // ===== ANALYTICS QUERIES =====

        public async Task<decimal> GetOccupancyRateAsync(
            int apartmentId,
            DateTime startDate,
            DateTime endDate)
        {
            // Get apartment to find total units
            var apartment = await _context.Apartments.FindAsync(apartmentId);
            if (apartment == null)
                return 0;

            var totalUnits = apartment.TotalUnits;
            var totalDays = (endDate - startDate).Days;
            
            if (totalDays <= 0)
                return 0;

            // Calculate total booked room-nights
            var bookings = await _dbSet
                .AsNoTracking()
                .Where(b => b.ApartmentId == apartmentId
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.CheckedIn)
                            && b.CheckIn < endDate
                            && b.CheckOut > startDate)
                .ToListAsync();

            var bookedRoomNights = 0;
            foreach (var booking in bookings)
            {
                var effectiveCheckIn = booking.CheckIn < startDate ? startDate : booking.CheckIn;
                var effectiveCheckOut = booking.CheckOut > endDate ? endDate : booking.CheckOut;
                var nights = (effectiveCheckOut - effectiveCheckIn).Days;
                bookedRoomNights += nights;
            }

            // Calculate occupancy rate
            var totalAvailableRoomNights = totalUnits * totalDays;
            var occupancyRate = totalAvailableRoomNights > 0 
                ? (decimal)bookedRoomNights / totalAvailableRoomNights * 100
                : 0;

            return Math.Round(occupancyRate, 2);
        }

        public async Task<decimal> GetRevenueAsync(
            int apartmentId,
            DateTime startDate,
            DateTime endDate)
        {
            var revenue = await _dbSet
                .AsNoTracking()
                .Where(b => b.ApartmentId == apartmentId
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.CheckedIn)
                            && b.CheckIn < endDate
                            && b.CheckOut > startDate)
                .SumAsync(b => b.TotalPrice);

            return revenue;
        }

        // ===== PRIVATE HELPER METHODS =====

        private List<string> GetAllRoomNumbersForType(RoomTypeEnum roomType)
        {
            var rooms = new List<string>();
            var prefix = roomType switch
            {
                RoomTypeEnum.Studio => "10",
                RoomTypeEnum.OneBedroom => "20",
                RoomTypeEnum.TwoBedroom => "30",
                _ => throw new ArgumentException($"Invalid room type: {roomType}")
            };

            // Rooms are numbered 101-113, 201-213, 301-313
            for (int i = 1; i <= 13; i++)
            {
                rooms.Add($"{prefix}{i}");
            }

            return rooms;
        }
    }
}