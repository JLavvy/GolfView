using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Services.Interfaces
{
    public interface IAvailabilityService
    {
        /// <summary>
        /// Returns apartments that have at least one room available for the given dates and guest count.
        /// </summary>
        Task<List<AvailableApartmentDto>> GetAvailableApartmentsAsync(
            DateTime checkIn,
            DateTime checkOut,
            int guests);

        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut);

        Task<List<Room>> GetAvailableRoomsAsync(
            int apartmentId,
            DateTime checkIn,
            DateTime checkOut);
    }
}