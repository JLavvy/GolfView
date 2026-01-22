using GolfViewApartments.API.Data;
using GolfViewApartments.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace GolfViewApartments.API.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        // Lazy initialization of repositories
        private IApartmentRepository? _apartments;
        private IBookingRepository? _bookings;
        private ICustomerRepository? _customers;
        private IRoomRepository? _rooms;
        private IContactMessageRepository? _contactMessages;
        private IContactInfoRepository? _contactInfo;
        private IPhotoRepository? _photos;
        private IAmenityPricingRepository? _amenityPricing;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // Repository properties with lazy initialization
        public IApartmentRepository Apartments
        {
            get
            {
                _apartments ??= new ApartmentRepository(_context);
                return _apartments;
            }
        }

        public IBookingRepository Bookings
        {
            get
            {
                _bookings ??= new BookingRepository(_context);
                return _bookings;
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                _customers ??= new CustomerRepository(_context);
                return _customers;
            }
        }

        public IRoomRepository Rooms
        {
            get
            {
                _rooms ??= new RoomRepository(_context);
                return _rooms;
            }
        }

        public IContactMessageRepository ContactMessages
        {
            get
            {
                _contactMessages ??= new ContactMessageRepository(_context);
                return _contactMessages;
            }
        }

        public IContactInfoRepository ContactInfo
        {
            get
            {
                _contactInfo ??= new ContactInfoRepository(_context);
                return _contactInfo;
            }
        }

        public IPhotoRepository Photos
        {
            get
            {
                _photos ??= new PhotoRepository(_context);
                return _photos;
            }
        }

        public IAmenityPricingRepository AmenityPricing
        {
            get
            {
                _amenityPricing ??= new AmenityPricingRepository(_context);
                return _amenityPricing;
            }
        }

        // Transaction management
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        // Dispose pattern
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}