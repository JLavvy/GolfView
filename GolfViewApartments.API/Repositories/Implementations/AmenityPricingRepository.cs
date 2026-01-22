using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Repositories.Implementations
{

public class AmenityPricingRepository : GenericRepository<AmenityPricing>, IAmenityPricingRepository
    {
        public AmenityPricingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<AmenityPricing?> GetByNameAsync(string name)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower());
        }
    }
}