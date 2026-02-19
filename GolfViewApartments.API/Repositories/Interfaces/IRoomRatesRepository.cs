using GolfViewApartments.API.DTOs;

namespace GolfViewApartments.API.Repositories.Interfaces
{
    public interface IRoomRatesRepository
    {
        /// <summary>
        /// Gets all rates for a given apartment type string ("studio", "one-bedroom", "two-bedroom")
        /// </summary>
        Task<List<RoomRateDto>> GetByApartmentTypeAsync(string apartmentType);

        /// <summary>
        /// Gets the lowest single occupancy / Bed Only rate for a given apartment type.
        /// Used as the "starting from" price in summaries.
        /// </summary>
        Task<decimal> GetStartingRateAsync(string apartmentType);
    }
}