using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Repositories.Interfaces
{
    public interface IContactMessageRepository : IGenericRepository<ContactMessage>
    {
        Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync();
        Task<IEnumerable<ContactMessage>> GetRecentMessagesAsync(int count = 10);

    }
}