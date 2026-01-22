using GolfViewApartments.API.Models;
namespace GolfViewApartments.API.Repositories.Interfaces
{
    public interface IPhotoRepository : IGenericRepository<Photo>
    {
        Task<IEnumerable<Photo>> GetByCategoryAsync(string category);
        Task<IEnumerable<Photo>> GetOrderedPhotosAsync();
    }
}