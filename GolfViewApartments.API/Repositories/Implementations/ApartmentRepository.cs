using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Repositories.Implementations
{
    public class ApartmentRepository : GenericRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Apartment?> GetByApartmentIdAsync(string apartmentId)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ApartmentId == apartmentId);
        }

        public async Task<IEnumerable<Apartment>> GetByTypeAsync(string type)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(a => a.Type == type)
                .ToListAsync();
        }
    }
}