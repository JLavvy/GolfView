using GolfViewApartments.API.Models;
using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.API.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for Booking entity operations
    /// Supports queries by apartment, room type, room number, and dates
    /// </summary>
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        // ===== CUSTOMER QUERIES =====
        
        /// <summary>
        /// Get all bookings for a specific customer
        /// </summary>
        Task<IEnumerable<Booking>> GetByCustomerIdAsync(int customerId);

        // ===== STATUS QUERIES =====
        
        /// <summary>
        /// Get bookings by status (Pending, Confirmed, Cancelled, etc.)
        /// </summary>
        Task<IEnumerable<Booking>> GetByStatusAsync(BookingStatus status);

        /// <summary>
        /// Get all active bookings (Confirmed, Pending, CheckedIn)
        /// </summary>
        Task<IEnumerable<Booking>> GetActiveBookingsAsync();

        // ===== APARTMENT QUERIES =====
        
        /// <summary>
        /// Get all bookings for a specific apartment
        /// </summary>
        Task<IEnumerable<Booking>> GetByApartmentIdAsync(int apartmentId);

        /// <summary>
        /// Get bookings for an apartment within a date range
        /// </summary>
        Task<IEnumerable<Booking>> GetBookingsByApartmentAndDateRangeAsync(
            int apartmentId, 
            DateTime checkIn, 
            DateTime checkOut);

        // ===== ROOM TYPE QUERIES =====
        
        /// <summary>
        /// Get bookings by room type within a date range
        /// </summary>
        Task<IEnumerable<Booking>> GetBookingsByRoomTypeAsync(
            RoomTypeEnum roomType, 
            DateTime checkIn, 
            DateTime checkOut);

        /// <summary>
        /// Get count of bookings for a room type in date range
        /// </summary>
        Task<int> GetBookingCountByRoomTypeAsync(
            RoomTypeEnum roomType, 
            DateTime checkIn, 
            DateTime checkOut);

        // ===== ROOM QUERIES =====
        
        /// <summary>
        /// Get bookings for a specific room number within date range
        /// </summary>
        Task<IEnumerable<Booking>> GetBookingsByRoomAsync(
            string roomNumber, 
            DateTime checkIn, 
            DateTime checkOut);

        // ===== DATE RANGE QUERIES =====
        
        /// <summary>
        /// Get all bookings within a date range (any apartment)
        /// </summary>
        Task<IEnumerable<Booking>> GetBookingsForDateRangeAsync(
            DateTime checkIn, 
            DateTime checkOut);

        // ===== AVAILABILITY CHECKS =====
        
        /// <summary>
        /// Check if a specific room is available for dates
        /// </summary>
        Task<bool> IsRoomAvailableForDatesAsync(
            string roomNumber,
            DateTime checkIn,
            DateTime checkOut,
            int? excludeBookingId = null);

        /// <summary>
        /// Check if room type has availability for dates
        /// </summary>
        Task<bool> IsRoomTypeAvailableForDatesAsync(
            RoomTypeEnum roomType,
            DateTime checkIn,
            DateTime checkOut,
            int? excludeBookingId = null);

        /// <summary>
        /// Check if apartment has availability for dates
        /// </summary>
        Task<bool> IsApartmentAvailableForDatesAsync(
            int apartmentId,
            DateTime checkIn,
            DateTime checkOut,
            int? excludeBookingId = null);

        /// <summary>
        /// Get list of available room numbers for a room type in date range
        /// </summary>
        Task<List<string>> GetAvailableRoomsAsync(
            RoomTypeEnum roomType,
            DateTime checkIn,
            DateTime checkOut);

        // ===== BOOKING REFERENCE QUERIES =====
        
        /// <summary>
        /// Get booking by unique booking reference code
        /// </summary>
        Task<Booking?> GetByBookingReferenceAsync(string bookingReference);

        // ===== ANALYTICS QUERIES =====
        
        /// <summary>
        /// Get occupancy rate for apartment in date range
        /// </summary>
        Task<decimal> GetOccupancyRateAsync(
            int apartmentId,
            DateTime startDate,
            DateTime endDate);

        /// <summary>
        /// Get revenue for apartment in date range
        /// </summary>
        Task<decimal> GetRevenueAsync(
            int apartmentId,
            DateTime startDate,
            DateTime endDate);
    }
}