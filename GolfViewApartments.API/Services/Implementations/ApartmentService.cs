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

                // Get rates for this apartment's room type from RoomRate table
                var rates = await _unitOfWork.RoomRates
                    .GetByApartmentTypeAsync(apartment.Type);

                apartmentDtos.Add(new ApartmentResponseDto
                {
                    Id = apartment.Id,
                    ApartmentId = apartment.ApartmentId,
                    Name = apartment.Name,
                    Type = apartment.Type,
                    Size = apartment.Size,
                    MaxGuests = apartment.MaxGuests,
                    TotalUnits = apartment.TotalUnits,
                    AvailableRooms = availableRooms,
                    Rates = rates
                });
            }

            return apartmentDtos;
        }

        public async Task<ApartmentResponseDto> GetApartmentByIdAsync(int id)
        {
            _logger.LogInformation("Fetching apartment with ID: {ApartmentId}", id);

            var apartment = await _unitOfWork.Apartments.GetByIdAsync(id);
            if (apartment == null)
                throw new NotFoundException(nameof(apartment), id);

            var availableRooms = await _unitOfWork.Rooms
                .CountAsync(r => r.ApartmentId == apartment.Id && r.IsAvailable);

            var rates = await _unitOfWork.RoomRates
                .GetByApartmentTypeAsync(apartment.Type);

            return new ApartmentResponseDto
            {
                Id = apartment.Id,
                ApartmentId = apartment.ApartmentId,
                Name = apartment.Name,
                Type = apartment.Type,
                Size = apartment.Size,
                MaxGuests = apartment.MaxGuests,
                TotalUnits = apartment.TotalUnits,
                AvailableRooms = availableRooms,
                Rates = rates
            };
        }

        public async Task<ApartmentResponseDto> GetApartmentByApartmentIdAsync(string apartmentId)
        {
            _logger.LogInformation("Fetching apartment with ApartmentId: {ApartmentId}", apartmentId);

            var apartment = await _unitOfWork.Apartments.GetByApartmentIdAsync(apartmentId);
            if (apartment == null)
                throw new NotFoundException($"Apartment with ApartmentId '{apartmentId}' not found");

            var availableRooms = await _unitOfWork.Rooms
                .CountAsync(r => r.ApartmentId == apartment.Id && r.IsAvailable);

            var rates = await _unitOfWork.RoomRates
                .GetByApartmentTypeAsync(apartment.Type);

            return new ApartmentResponseDto
            {
                Id = apartment.Id,
                ApartmentId = apartment.ApartmentId,
                Name = apartment.Name,
                Type = apartment.Type,
                Size = apartment.Size,
                MaxGuests = apartment.MaxGuests,
                TotalUnits = apartment.TotalUnits,
                AvailableRooms = availableRooms,
                Rates = rates
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

                // Starting price = lowest SingleOccupancy rate (Bed Only) for this room type
                var startingRate = await _unitOfWork.RoomRates
                    .GetStartingRateAsync(apartment.Type);

                summaries.Add(new ApartmentSummaryDto
                {
                    Id = apartment.Id,
                    Name = apartment.Name,
                    Type = apartment.Type,
                    AvailableRooms = availableRooms,
                    StartingPrice = startingRate
                });
            }

            return summaries;
        }

        // UpdateApartmentPricingAsync is removed — pricing is now managed via
        // PricingController (PUT api/pricing/roomtypes) against the RoomRate table.
        // If you still need this endpoint, update it through IUnitOfWork.RoomRates instead.
    }
}