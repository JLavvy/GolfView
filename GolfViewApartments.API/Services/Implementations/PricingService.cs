using GolfViewApartments.API.Data;
using GolfViewApartments.API.Services.Interfaces;
using GolfViewApartments.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Services
{
    /// <summary>
    /// Pricing service that strictly uses RoomRate table values.
    /// No discounts. No multipliers. No automatic adjustments.
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
    var nights = (checkOut - checkIn).Days;

    if (nights <= 0)
        throw new ArgumentException("Check-out must be after check-in");

    var roomTypeEntity = await _context.RoomTypes
        .FirstOrDefaultAsync(rt => rt.RoomTypeEnum == roomType);

    if (roomTypeEntity == null)
        throw new ArgumentException($"Room type '{roomType}' not found");

    var rate = await _context.RoomRates
        .FirstOrDefaultAsync(r =>
            r.RoomTypeId == roomTypeEntity.Id &&
            r.BoardType == boardType);

    if (rate == null)
        throw new ArgumentException(
            $"Rate not found for room type '{roomType}' with board type '{boardType}'");

    decimal baseRatePerNight;

    if (roomType == RoomTypeEnum.TwoBedroom)
    {
        // TwoBedroom:
        // FirstOccupancy = Double
        // SecondOccupancy = Quadruple
        baseRatePerNight = occupancy switch
        {
            "Double" => rate.FirstOccupancy,
            "Quadruple" => rate.SecondOccupancy,
            _ => throw new ArgumentException("Two Bedroom supports Double or Quadruple only")
        };
    }
    else
    {
        // Studio & OneBedroom:
        // FirstOccupancy = Single
        // SecondOccupancy = Double
        baseRatePerNight = occupancy switch
        {
            "Single" => rate.FirstOccupancy,
            "Double" => rate.SecondOccupancy,
            _ => rate.SecondOccupancy
        };
    }

    var total = baseRatePerNight * nights;

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

            // Return SecondOccupancy as default base rate
            return rate.SecondOccupancy;
        }

        public decimal GetOccupancyMultiplier(string occupancy)
        {
            // No multipliers used anymore
            return 1.0m;
        }

        public decimal GetLengthOfStayDiscount(int nights)
        {
            // No discounts
            return 0m;
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

                return calculatedPrice == providedPrice;
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
    }
}
