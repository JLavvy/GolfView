using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfViewApartments.API.Data;
using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Models;
using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PricingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================================
        // ROOM TYPES AND RATES
        // ============================================

        // GET: api/pricing/roomtypes
        [HttpGet("roomtypes")]
        public async Task<ActionResult<List<RoomTypeDto>>> GetRoomTypes()
        {
            var roomTypes = await _context.RoomTypes
                .Include(rt => rt.Rates)
                .ToListAsync();

            var result = roomTypes.Select(rt => new RoomTypeDto
            {
                Id = rt.Id,
                Name = rt.Name,
                RoomTypeEnum = rt.RoomTypeEnum,
                IconClass = rt.IconClass,
                Rates = rt.Rates.Select(r => new RoomRateDto
                {
                    Id = r.Id,
                    RoomTypeId = r.RoomTypeId,
                    BoardType = r.BoardType,
                    FirstOccupancy = r.FirstOccupancy,
                    SecondOccupancy = r.SecondOccupancy
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        // PUT: api/pricing/roomtypes
        [HttpPut("roomtypes")]
        public async Task<IActionResult> UpdateRoomTypes(List<RoomTypeDto> roomTypeDtos)
        {
            foreach (var roomTypeDto in roomTypeDtos)
            {
                var existingRoomType = await _context.RoomTypes
                    .Include(rt => rt.Rates)
                    .FirstOrDefaultAsync(rt => rt.Id == roomTypeDto.Id);

                if (existingRoomType != null)
                {
                    // Update room type basic info
                    existingRoomType.Name = roomTypeDto.Name;
                    existingRoomType.RoomTypeEnum = roomTypeDto.RoomTypeEnum;
                    existingRoomType.IconClass = roomTypeDto.IconClass;

                    // Update rates
                    foreach (var rateDto in roomTypeDto.Rates)
                    {
                        var existingRate = existingRoomType.Rates
                            .FirstOrDefault(r => r.Id == rateDto.Id);

                        if (existingRate != null)
                        {
                            existingRate.BoardType = rateDto.BoardType;
                            existingRate.FirstOccupancy = rateDto.FirstOccupancy;
                            existingRate.SecondOccupancy = rateDto.SecondOccupancy;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ============================================
        // CONFERENCE PACKAGES
        // ============================================

        // GET: api/pricing/conference
        [HttpGet("conference")]
        public async Task<ActionResult<List<ConferencePackageDto>>> GetConferencePackages()
        {
            var packages = await _context.ConferencePackages.ToListAsync();

            var result = packages.Select(p => new ConferencePackageDto
            {
                Id = p.Id,
                Name = p.Name,
                IconClass = p.IconClass,
                Price = p.Price
            }).ToList();

            return Ok(result);
        }

        // PUT: api/pricing/conference
        [HttpPut("conference")]
        public async Task<IActionResult> UpdateConferencePackages(List<ConferencePackageDto> packageDtos)
        {
            foreach (var dto in packageDtos)
            {
                var existing = await _context.ConferencePackages
                    .FirstOrDefaultAsync(p => p.Id == dto.Id);

                if (existing != null)
                {
                    existing.Name = dto.Name;
                    existing.IconClass = dto.IconClass;
                    existing.Price = dto.Price;
                    existing.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    _context.ConferencePackages.Add(new ConferencePackage
                    {
                        Name = dto.Name,
                        IconClass = dto.IconClass,
                        Price = dto.Price,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ============================================
        // FITNESS AMENITIES
        // ============================================

        // GET: api/pricing/fitness
        [HttpGet("fitness")]
        public async Task<ActionResult<List<FitnessAmenityDto>>> GetFitnessAmenities()
        {
            var amenities = await _context.FitnessAmenities.ToListAsync();

            var result = amenities.Select(a => new FitnessAmenityDto
            {
                Id = a.Id,
                Name = a.Name,
                IconClass = a.IconClass,
                DayRate = a.DayRate,
                MonthlyRate = a.MonthlyRate
            }).ToList();

            return Ok(result);
        }

        // PUT: api/pricing/fitness
        [HttpPut("fitness")]
        public async Task<IActionResult> UpdateFitnessAmenities(List<FitnessAmenityDto> amenityDtos)
        {
            foreach (var dto in amenityDtos)
            {
                var existing = await _context.FitnessAmenities
                    .FirstOrDefaultAsync(a => a.Id == dto.Id);

                if (existing != null)
                {
                    existing.Name = dto.Name;
                    existing.IconClass = dto.IconClass;
                    existing.DayRate = dto.DayRate;
                    existing.MonthlyRate = dto.MonthlyRate;
                    existing.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    _context.FitnessAmenities.Add(new FitnessAmenity
                    {
                        Name = dto.Name,
                        IconClass = dto.IconClass,
                        DayRate = dto.DayRate,
                        MonthlyRate = dto.MonthlyRate,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ============================================
        // LEGACY ENDPOINTS (for backward compatibility)
        // You can remove these if not needed anymore
        // ============================================

        // GET: api/pricing/rooms
        [HttpGet("rooms")]
        [Obsolete("Use /roomtypes endpoint instead")]
        public async Task<ActionResult<List<RoomRate>>> GetRoomRates()
        {
            return await _context.RoomRates.ToListAsync();
        }

        // GET: api/pricing/amenities
        [HttpGet("amenities")]
        [Obsolete("Use /fitness endpoint instead")]
        public async Task<ActionResult<List<AmenityPricing>>> GetAmenities()
        {
            return await _context.AmenityPricing.ToListAsync();
        }
    }
}
