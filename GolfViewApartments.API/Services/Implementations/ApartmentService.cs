using GolfViewApartments.API.Common.Exceptions;
using GolfViewApartments.API.DTOs.Apartment;
using GolfViewApartments.API.Repositories.Interfaces;
using GolfViewApartments.API.Services.Interfaces;

namespace GolfViewApartments.API.Services.Implementations
{
    public class ApartmentService : IApartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApartmentService> _logger;

        public ApartmentService(IUnitOfWork unitOfWork, ILogger<ApartmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<ApartmentResponseDto>> GetAllApartmentsAsync()
        {
            _logger.LogInformation("Fetching all apartments");

            var apartments = await _unitOfWork.Apartments.GetAllAsync();
            var apartmentDtos = new List<ApartmentResponseDto>();

            foreach (var apartment in apartments)
            {
                var availableRooms = await _unitOfWork.Rooms
                    .CountAsync(r => r.ApartmentId == apartment.Id && r.IsAvailable);

                apartmentDtos.Add(new ApartmentResponseDto
                {
                    Id = apartment.Id,
                    ApartmentId = apartment.ApartmentId,
                    Name = apartment.Name,
                    Type = apartment.Type,
                    Size = apartment.Size,
                    MaxGuests = apartment.MaxGuests,
                    TotalUnits = apartment.TotalUnits,
                    DailyBedOnly = apartment.DailyBedOnly,
                    DailyBB = apartment.DailyBB,
                    MonthlyBedOnly = apartment.MonthlyBedOnly,
                    MonthlyBB = apartment.MonthlyBB,
                    AvailableRooms = availableRooms
                });
            }

            return apartmentDtos;
        }

        public async Task<ApartmentResponseDto> GetApartmentByIdAsync(int id)
        {
            _logger.LogInformation("Fetching apartment with ID: {ApartmentId}", id);

            var apartment = await _unitOfWork.Apartments.GetByIdAsync(id);
            if (apartment == null)
            {
                throw new NotFoundException(nameof(apartment), id);
            }

            var availableRooms = await _unitOfWork.Rooms
                .CountAsync(r => r.ApartmentId == apartment.Id && r.IsAvailable);

            return new ApartmentResponseDto
            {
                Id = apartment.Id,
                ApartmentId = apartment.ApartmentId,
                Name = apartment.Name,
                Type = apartment.Type,
                Size = apartment.Size,
                MaxGuests = apartment.MaxGuests,
                TotalUnits = apartment.TotalUnits,
                DailyBedOnly = apartment.DailyBedOnly,
                DailyBB = apartment.DailyBB,
                MonthlyBedOnly = apartment.MonthlyBedOnly,
                MonthlyBB = apartment.MonthlyBB,
                AvailableRooms = availableRooms
            };
        }

        public async Task<ApartmentResponseDto> GetApartmentByApartmentIdAsync(string apartmentId)
        {
            _logger.LogInformation("Fetching apartment with ApartmentId: {ApartmentId}", apartmentId);

            var apartment = await _unitOfWork.Apartments.GetByApartmentIdAsync(apartmentId);
            if (apartment == null)
            {
                throw new NotFoundException($"Apartment with ApartmentId '{apartmentId}' not found");
            }

            var availableRooms = await _unitOfWork.Rooms
                .CountAsync(r => r.ApartmentId == apartment.Id && r.IsAvailable);

            return new ApartmentResponseDto
            {
                Id = apartment.Id,
                ApartmentId = apartment.ApartmentId,
                Name = apartment.Name,
                Type = apartment.Type,
                Size = apartment.Size,
                MaxGuests = apartment.MaxGuests,
                TotalUnits = apartment.TotalUnits,
                DailyBedOnly = apartment.DailyBedOnly,
                DailyBB = apartment.DailyBB,
                MonthlyBedOnly = apartment.MonthlyBedOnly,
                MonthlyBB = apartment.MonthlyBB,
                AvailableRooms = availableRooms
            };
        }

        public async Task<IEnumerable<ApartmentSummaryDto>> GetApartmentSummariesAsync()
        {
            _logger.LogInformation("Fetching apartment summaries");

            var apartments = await _unitOfWork.Apartments.GetAllAsync();
            var summaries = new List<ApartmentSummaryDto>();

            foreach (var apartment in apartments)
            {
                var availableRooms = await _unitOfWork.Rooms
                    .CountAsync(r => r.ApartmentId == apartment.Id && r.IsAvailable);

                summaries.Add(new ApartmentSummaryDto
                {
                    Id = apartment.Id,
                    Name = apartment.Name,
                    Type = apartment.Type,
                    AvailableRooms = availableRooms,
                    StartingPrice = apartment.DailyBedOnly
                });
            }

            return summaries;
        }

        public async Task UpdateApartmentPricingAsync(int id, UpdateApartmentPricingDto dto)
        {
            _logger.LogInformation("Updating pricing for apartment ID: {ApartmentId}", id);

            var apartment = await _unitOfWork.Apartments.GetByIdAsync(id);
            if (apartment == null)
            {
                throw new NotFoundException(nameof(apartment), id);
            }

            // Update pricing
            apartment.DailyBedOnly = dto.DailyBedOnly;
            apartment.DailyBB = dto.DailyBB;
            apartment.MonthlyBedOnly = dto.MonthlyBedOnly;
            apartment.MonthlyBB = dto.MonthlyBB;

            _unitOfWork.Apartments.Update(apartment);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Successfully updated pricing for apartment ID: {ApartmentId}", id);
        }
    }
}