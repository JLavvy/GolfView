using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Repositories.Implementations
{
public class ContactInfoRepository : GenericRepository<ContactInfo>, IContactInfoRepository
    {
        public ContactInfoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ContactInfo?> GetContactInfoAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}