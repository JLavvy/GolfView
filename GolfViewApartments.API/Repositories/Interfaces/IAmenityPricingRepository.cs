using GolfViewApartments.API.Models;
namespace GolfViewApartments.API.Repositories.Interfaces
{
    public interface IAmenityPricingRepository : IGenericRepository<AmenityPricing>
    {
    Task<AmenityPricing?> GetByNameAsync(string name);
    }
}