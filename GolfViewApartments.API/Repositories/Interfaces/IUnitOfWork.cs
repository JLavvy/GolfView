namespace GolfViewApartments.API.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IApartmentRepository Apartments { get; }
        IBookingRepository Bookings { get; }
        ICustomerRepository Customers { get; }
        IRoomRepository Rooms { get; }
        IContactMessageRepository ContactMessages { get; }
        IContactInfoRepository ContactInfo { get; }
        IPhotoRepository Photos { get; }
        IAmenityPricingRepository AmenityPricing { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}