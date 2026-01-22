using GolfViewApartments.API.Models;
namespace GolfViewApartments.API.Repositories.Interfaces
{
    public interface IContactInfoRepository : IGenericRepository<ContactInfo>
    {
        Task<ContactInfo?> GetContactInfoAsync();
    }
}