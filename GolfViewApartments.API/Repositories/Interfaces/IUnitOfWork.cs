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

        /// <summary>
        /// Room rates repository — replaces the old per-column pricing on Apartment.
        /// Used by ApartmentService to fetch rates by apartment type.
        /// </summary>
        IRoomRatesRepository RoomRates { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}