using GolfViewApartments.API.Data;
using GolfViewApartments.API.Services.Interfaces;
using GolfViewApartments.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using GolfViewApartments.API.Models;


namespace GolfViewApartments.API.Services
{
    /// <summary>
    /// Pricing service that uses YOUR existing RoomRate table
    /// No hardcoded rates - everything comes from the database!
    /// </summary>
    public class PricingService : IPricingService
    {
        private readonly ApplicationDbContext _context;

        public PricingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> CalculateBookingPriceAsync(
            RoomTypeEnum roomType,
            string boardType,
            string occupancy,
            DateTime checkIn,
            DateTime checkOut,
            int adults,
            int children)
        {
            // Step 1: Validate dates
            var nights = (checkOut - checkIn).Days;
            if (nights <= 0)
                throw new ArgumentException("Check-out must be after check-in");

            // Step 2: Get RoomType from YOUR database
            var roomTypeEntity = await _context.RoomTypes
                .Include(rt => rt.Rates)
                .FirstOrDefaultAsync(rt => rt.RoomTypeEnum == roomType);

            if (roomTypeEntity == null)
                throw new ArgumentException($"Room type '{roomType}' not found in database");

            // Step 3: Get rate from YOUR RoomRate table
            var rate = await _context.RoomRates
                .FirstOrDefaultAsync(r => 
                    r.RoomTypeId == roomTypeEntity.Id &&
                    r.BoardType == boardType);

            if (rate == null)
            {
                throw new ArgumentException(
                    $"Rate not found for room type '{roomType}' with board type '{boardType}'. " +
                    $"Please ensure this combination exists in the RoomRate table.");
            }

            // Step 4: Determine which rate to use based on occupancy
            decimal baseRatePerNight = GetBaseRateFromOccupancy(
                rate, 
                occupancy, 
                adults + children);

            // Step 5: Calculate subtotal
            var subtotal = baseRatePerNight * nights;

            // Step 6: Apply length of stay discounts (optional)
            var discount = GetLengthOfStayDiscount(nights);
            var total = subtotal * (1 - discount);

            return Math.Round(total, 2);
        }

        public async Task<decimal> GetBaseRateAsync(RoomTypeEnum roomType, string boardType)
        {
            var roomTypeEntity = await _context.RoomTypes
                .FirstOrDefaultAsync(rt => rt.RoomTypeEnum == roomType);

            if (roomTypeEntity == null)
                throw new ArgumentException($"Room type '{roomType}' not found");

            var rate = await _context.RoomRates
                .FirstOrDefaultAsync(r => 
                    r.RoomTypeId == roomTypeEntity.Id &&
                    r.BoardType == boardType);

            if (rate == null)
                throw new ArgumentException($"Rate not found for {roomType} - {boardType}");

            // Return SecondOccupancy as the "base" rate
            return rate.SecondOccupancy;
        }

        public decimal GetOccupancyMultiplier(string occupancy)
        {
            // Not used in this implementation - we use FirstOccupancy/SecondOccupancy directly
            // Kept for interface compatibility
            return occupancy switch
            {
                "Single" => 1.0m,
                "Double" => 1.0m,
                "Quadruple" => 1.4m,
                _ => 1.0m
            };
        }

        public decimal GetLengthOfStayDiscount(int nights)
        {
            // Optional: Apply discounts for longer stays
            // You can adjust these percentages based on your business needs
            if (nights >= 30)
                return 0.20m;      // 20% off for 30+ nights
            if (nights >= 14)
                return 0.15m;      // 15% off for 14-29 nights
            if (nights >= 7)
                return 0.10m;      // 10% off for 7-13 nights
            if (nights >= 3)
                return 0.05m;      // 5% off for 3-6 nights

            return 0m;             // No discount for 1-2 nights
        }

        public async Task<bool> ValidatePriceAsync(
            RoomTypeEnum roomType,
            string boardType,
            string occupancy,
            DateTime checkIn,
            DateTime checkOut,
            int adults,
            int children,
            decimal providedPrice)
        {
            try
            {
                var calculatedPrice = await CalculateBookingPriceAsync(
                    roomType,
                    boardType,
                    occupancy,
                    checkIn,
                    checkOut,
                    adults,
                    children);

                // Allow for small rounding differences (within $1)
                var difference = Math.Abs(calculatedPrice - providedPrice);
                return difference <= 1.0m;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RateExistsAsync(RoomTypeEnum roomType, string boardType)
        {
            var roomTypeEntity = await _context.RoomTypes
                .FirstOrDefaultAsync(rt => rt.RoomTypeEnum == roomType);

            if (roomTypeEntity == null)
                return false;

            return await _context.RoomRates
                .AnyAsync(r => 
                    r.RoomTypeId == roomTypeEntity.Id &&
                    r.BoardType == boardType);
        }

        public async Task<List<string>> GetAvailableBoardTypesAsync(RoomTypeEnum roomType)
        {
            var roomTypeEntity = await _context.RoomTypes
                .FirstOrDefaultAsync(rt => rt.RoomTypeEnum == roomType);

            if (roomTypeEntity == null)
                return new List<string>();

            return await _context.RoomRates
                .Where(r => r.RoomTypeId == roomTypeEntity.Id)
                .Select(r => r.BoardType)
                .Distinct()
                .ToListAsync();
        }

        // ===== PRIVATE HELPER METHODS =====

        /// <summary>
        /// Get the appropriate rate from RoomRate table based on occupancy
        /// Uses YOUR existing FirstOccupancy and SecondOccupancy columns
        /// </summary>
        private decimal GetBaseRateFromOccupancy(
            RoomRate rate, 
            string occupancy, 
            int totalGuests)
        {
            return occupancy switch
            {
                "Single" => rate.FirstOccupancy,
                
                "Double" => rate.SecondOccupancy,
                
                "Quadruple" => rate.SecondOccupancy * 1.4m,
                // For 4 people, use SecondOccupancy rate with 40% surcharge
                // You can adjust this multiplier based on your business rules
                
                _ => totalGuests <= 1 
                    ? rate.FirstOccupancy 
                    : rate.SecondOccupancy
            };
        }
    }
}