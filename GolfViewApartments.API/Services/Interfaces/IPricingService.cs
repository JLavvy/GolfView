using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.API.Services.Interfaces
{
    /// <summary>
    /// Service for calculating booking prices using YOUR existing RoomRate table
    /// </summary>
    public interface IPricingService
    {
        /// <summary>
        /// Calculate total booking price by querying RoomRate table
        /// </summary>
        /// <param name="roomType">Room type enum (matches RoomType.RoomTypeEnum)</param>
        /// <param name="boardType">Board type (must exist in RoomRate.BoardType)</param>
        /// <param name="occupancy">Occupancy level (Single, Double, Quadruple)</param>
        /// <param name="checkIn">Check-in date</param>
        /// <param name="checkOut">Check-out date</param>
        /// <param name="adults">Number of adults</param>
        /// <param name="children">Number of children</param>
        /// <returns>Total calculated price</returns>
        Task<decimal> CalculateBookingPriceAsync(
            RoomTypeEnum roomType,
            string boardType,
            string occupancy,
            DateTime checkIn,
            DateTime checkOut,
            int adults,
            int children);

        /// <summary>
        /// Get base rate per night from RoomRate table
        /// </summary>
        Task<decimal> GetBaseRateAsync(RoomTypeEnum roomType, string boardType);

        /// <summary>
        /// Get occupancy multiplier (not really used since we query FirstOccupancy/SecondOccupancy)
        /// </summary>
        decimal GetOccupancyMultiplier(string occupancy);

        /// <summary>
        /// Calculate discount based on length of stay
        /// </summary>
        decimal GetLengthOfStayDiscount(int nights);

        /// <summary>
        /// Validate if provided price matches calculated price
        /// </summary>
        Task<bool> ValidatePriceAsync(
            RoomTypeEnum roomType,
            string boardType,
            string occupancy,
            DateTime checkIn,
            DateTime checkOut,
            int adults,
            int children,
            decimal providedPrice);

        /// <summary>
        /// Check if a rate exists in RoomRate table
        /// </summary>
        Task<bool> RateExistsAsync(RoomTypeEnum roomType, string boardType);

        /// <summary>
        /// Get list of available board types for a room type
        /// </summary>
        Task<List<string>> GetAvailableBoardTypesAsync(RoomTypeEnum roomType);
    }
}