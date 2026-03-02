using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GolfViewApartments.API.Data;

namespace GolfViewApartments.API.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================================
        // ADD
        // ================================
        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        // ================================
        // GET BY ID
        // ================================
        public async Task<Customer?> GetByIdAsync(int id)
        {
            // No AsNoTracking() here → tracked entity
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // ================================
        // GET BY EMAIL
        // ================================
        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
        }

        // ================================
        // GET ALL
        // ================================
        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        // ================================
        // UPDATE
        // ================================
        public async Task UpdateAsync(Customer customer)
        {
            // Make sure the entity is attached
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        // ================================
        // DELETE
        // ================================
        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}