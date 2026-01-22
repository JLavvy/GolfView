using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Repositories.Interfaces
{
    public interface IApartmentRepository : IGenericRepository<Apartment>
    {
        Task<Apartment?> GetByApartmentIdAsync(string apartmentId);
        Task<IEnumerable<Apartment>> GetByTypeAsync(string type);
    }
}