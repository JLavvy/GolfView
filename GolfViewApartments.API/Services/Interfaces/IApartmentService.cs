using GolfViewApartments.API.DTOs.Apartment;

namespace GolfViewApartments.API.Services.Interfaces
{
    public interface IApartmentService
    {
        Task<IEnumerable<ApartmentResponseDto>> GetAllApartmentsAsync();
        Task<ApartmentResponseDto> GetApartmentByIdAsync(int id);
        Task<ApartmentResponseDto> GetApartmentByApartmentIdAsync(string apartmentId);
        Task<IEnumerable<ApartmentSummaryDto>> GetApartmentSummariesAsync();

        // UpdateApartmentPricingAsync removed — pricing is managed via
        // PricingController (PUT api/pricing/roomtypes) against the RoomRate table.
    }
}