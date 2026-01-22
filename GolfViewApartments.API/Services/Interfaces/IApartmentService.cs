using GolfViewApartments.API.DTOs.Apartment;

namespace GolfViewApartments.API.Services.Interfaces
{
    public interface IApartmentService
    {
        Task<IEnumerable<ApartmentResponseDto>> GetAllApartmentsAsync();
        Task<ApartmentResponseDto> GetApartmentByIdAsync(int id);
        Task<ApartmentResponseDto> GetApartmentByApartmentIdAsync(string apartmentId);
        Task<IEnumerable<ApartmentSummaryDto>> GetApartmentSummariesAsync();
        Task UpdateApartmentPricingAsync(int id, UpdateApartmentPricingDto dto);
    }
}