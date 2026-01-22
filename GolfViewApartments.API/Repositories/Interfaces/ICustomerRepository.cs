using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer?> GetByEmailAsync(string email);
        Task<Customer?> GetByPhoneAsync(string phone);
        Task<bool> EmailExistsAsync(string email);
    }
}