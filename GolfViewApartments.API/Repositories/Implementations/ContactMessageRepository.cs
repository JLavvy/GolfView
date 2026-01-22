using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Repositories.Implementations
{
    public class ContactMessageRepository
        : GenericRepository<ContactMessage>, IContactMessageRepository
    {
        public ContactMessageRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(m => !m.IsRead)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ContactMessage>> GetRecentMessagesAsync(int count = 10)
        {
            return await _dbSet
                .AsNoTracking()
                .OrderByDescending(m => m.CreatedAt)
                .Take(count)
                .ToListAsync();
        }
    }
}
