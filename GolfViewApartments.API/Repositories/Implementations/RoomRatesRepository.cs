using GolfViewApartments.API.Data;
using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Repositories.Interfaces;
using GolfViewApartments.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Repositories.Implementations
{
    public class RoomRatesRepository : IRoomRatesRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRatesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoomRateDto>> GetByApartmentTypeAsync(string apartmentType)
        {
            var roomTypeEnum = MapApartmentTypeToEnum(apartmentType);

            var rates = await _context.RoomRates
                .Include(r => r.RoomType)
                .Where(r => r.RoomType.RoomTypeEnum == roomTypeEnum)
                .Select(r => new RoomRateDto
                {
                    Id = r.Id,
                    RoomTypeId = r.RoomTypeId,
                    BoardType = r.BoardType,
                    FirstOccupancy = r.FirstOccupancy,
                    SecondOccupancy = r.SecondOccupancy
                })
                .ToListAsync();

            return rates;
        }

        public async Task<decimal> GetStartingRateAsync(string apartmentType)
        {
            var roomTypeEnum = MapApartmentTypeToEnum(apartmentType);

            var lowestRate = await _context.RoomRates
                .Include(r => r.RoomType)
                .Where(r => r.RoomType.RoomTypeEnum == roomTypeEnum)
                .MinAsync(r => (decimal?)r.FirstOccupancy);

            return lowestRate ?? 0m;
        }

        private static RoomTypeEnum MapApartmentTypeToEnum(string apartmentType) =>
            apartmentType.ToLower() switch
            {
                "studio"       => RoomTypeEnum.Studio,
                "one-bedroom"  => RoomTypeEnum.OneBedroom,
                "two-bedroom"  => RoomTypeEnum.TwoBedroom,
                _ => throw new ArgumentException($"Unknown apartment type: {apartmentType}")
            };
    }
}