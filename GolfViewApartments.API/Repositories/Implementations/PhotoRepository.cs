using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Repositories.Implementations
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Photo>> GetByCategoryAsync(string category)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(p => p.Category == category)
                .OrderBy(p => p.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<Photo>> GetOrderedPhotosAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .OrderBy(p => p.DisplayOrder)
                .ThenByDescending(p => p.UploadedAt)
                .ToListAsync();
        }
    }
}