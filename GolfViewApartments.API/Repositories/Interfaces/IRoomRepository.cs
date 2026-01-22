using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Repositories.Interfaces
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<Room?> GetByRoomNumberAsync(string roomNumber);
        Task<IEnumerable<Room>> GetByTypeAsync(string roomType);
        Task<IEnumerable<Room>> GetByApartmentIdAsync(int apartmentId);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
        Task<IEnumerable<Room>> GetAvailableRoomsForDatesAsync(int apartmentId, DateTime checkIn, DateTime checkOut);
    }
}