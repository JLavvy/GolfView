using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Repositories.Implementations
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
        }

        public async Task<Customer?> GetByPhoneAsync(string phone)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Phone == phone);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(c => c.Email.ToLower() == email.ToLower());
        }
    }
}