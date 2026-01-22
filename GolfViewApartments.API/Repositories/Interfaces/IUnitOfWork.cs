namespace GolfViewApartments.API.Repositories.Interfaces
{
    /// <summary>
    /// Unit of Work pattern to manage transactions across multiple repositories
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // Repositories
        IApartmentRepository Apartments { get; }
        IBookingRepository Bookings { get; }
        ICustomerRepository Customers { get; }
        IRoomRepository Rooms { get; }
        IContactMessageRepository ContactMessages { get; }
        IContactInfoRepository ContactInfo { get; }
        IPhotoRepository Photos { get; }
        IAmenityPricingRepository AmenityPricing { get; }

        // Transaction methods
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}