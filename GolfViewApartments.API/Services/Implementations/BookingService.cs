using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Services.Interfaces;
using GolfViewApartments.API.Data;
using Microsoft.EntityFrameworkCore;
using GolfViewApartments.Shared.Enums;

namespace GolfViewApartments.API.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPricingService _pricingService;
        private readonly ILogger<BookingService> _logger;

        public BookingService(
            ApplicationDbContext context,
            IPricingService pricingService,
            ILogger<BookingService> logger)
        {
            _context = context;
            _pricingService = pricingService;
            _logger = logger;
        }

        private async Task<int> GetOrCreateCustomerAsync(
            string firstName, string lastName, string email, string phone)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email);

            if (customer == null)
            {
                customer = new Customer
                {
                    FirstName = firstName,
                    LastName  = lastName,
                    Email     = email,
                    Phone     = phone,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            return customer.Id;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto request)
        {
            // 1. Validate dates
            if (request.CheckOut <= request.CheckIn)
                throw new ArgumentException("Check-out date must be after check-in date");

            // 2. Get or create customer
            if (request.CustomerId == 0)
            {
                var nameParts = request.Customer.Split(' ', 2);
                var firstName = nameParts.Length > 0 ? nameParts[0] : request.Customer;
                var lastName  = nameParts.Length > 1 ? nameParts[1] : "";

                request.CustomerId = await GetOrCreateCustomerAsync(
                    firstName, lastName, request.Email, request.Phone);
            }

            // 3. Resolve apartment
            Apartment? apartment = null;

            if (request.ApartmentId > 0)
            {
                apartment = await _context.Apartments.FindAsync(request.ApartmentId);
            }
            else
            {
                var apartmentType = request.RoomType switch
                {
                    RoomTypeEnum.Studio     => "studio",
                    RoomTypeEnum.OneBedroom => "one-bedroom",
                    RoomTypeEnum.TwoBedroom => "two-bedroom",
                    _ => throw new ArgumentException($"Invalid room type: {request.RoomType}")
                };

                apartment = await _context.Apartments
                    .FirstOrDefaultAsync(a => a.Type == apartmentType);

                if (apartment != null)
                    request.ApartmentId = apartment.Id;
            }

            if (apartment == null)
                throw new ArgumentException("Could not find apartment for selected room type");

            // 4. Validate RoomType matches apartment
            var expectedRoomType = MapApartmentTypeToEnum(apartment.Type);
            if (request.RoomType != expectedRoomType)
                throw new ArgumentException(
                    $"Room type mismatch: apartment is '{apartment.Type}' " +
                    $"but '{request.RoomType}' was specified.");

            // 5. Validate or assign room
            if (!string.IsNullOrEmpty(request.Room))
            {
                // Guest chose a specific room — validate it is actually available
                var isAvailable = await IsRoomAvailableAsync(
                    request.Room, request.CheckIn, request.CheckOut);

                if (!isAvailable)
                    throw new ArgumentException(
                        $"Room {request.Room} is not available for the selected dates. " +
                        "Please go back and choose a different room.");
            }
            else
            {
                // No room chosen — auto-assign the first available one
                var availableRooms = await GetAvailableRoomsForBookingAsync(
                    request.RoomType, request.CheckIn, request.CheckOut);

                if (!availableRooms.Any())
                    throw new ArgumentException(
                        $"No {request.RoomType} rooms are available for the selected dates");

                request.Room = availableRooms.First().RoomNumber;
            }

            // 6. Validate guest count against occupancy
            var totalGuests           = request.Adults + request.Children;
            var maxGuestsForOccupancy = GetMaxGuestsForOccupancy(request.Occupancy);

            if (totalGuests > maxGuestsForOccupancy)
                throw new ArgumentException(
                    $"{request.Occupancy} occupancy supports maximum {maxGuestsForOccupancy} guests. " +
                    $"You have {totalGuests} guests.");

            // 7. Validate guest count against room capacity
            var maxCapacity = GetMaxCapacityForRoomType(request.RoomType);
            if (totalGuests > maxCapacity)
                throw new ArgumentException(
                    $"{request.RoomType} room supports maximum {maxCapacity} guests. " +
                    $"You have {totalGuests} guests.");

            // 8. Validate rate exists
            var rateExists = await _pricingService.RateExistsAsync(request.RoomType, request.BoardType);
            if (!rateExists)
            {
                var available = await _pricingService.GetAvailableBoardTypesAsync(request.RoomType);
                throw new ArgumentException(
                    $"Rate not found for {request.RoomType} with board type '{request.BoardType}'. " +
                    $"Available: {string.Join(", ", available)}");
            }

            // 9. Calculate price
            var calculatedPrice = await _pricingService.CalculateBookingPriceAsync(
                request.RoomType,
                request.BoardType,
                request.Occupancy,
                request.CheckIn,
                request.CheckOut,
                request.Adults,
                request.Children);

            // 10. Validate or accept provided price
            decimal finalPrice;
            if (request.TotalPrice.HasValue && request.TotalPrice.Value > 0)
            {
                var diff = Math.Abs(request.TotalPrice.Value - calculatedPrice);
                if (diff > 1.0m)
                {
                    _logger.LogWarning(
                        "Price mismatch: provided {Provided}, calculated {Calculated}",
                        request.TotalPrice.Value, calculatedPrice);
                    throw new ArgumentException(
                        $"Price mismatch: provided {request.TotalPrice.Value:F2} " +
                        $"does not match calculated {calculatedPrice:F2}");
                }
                finalPrice = calculatedPrice;
            }
            else
            {
                finalPrice = calculatedPrice;
            }

            // 11. Create booking
            var reference = GenerateBookingReference();

            var booking = new Booking
            {
                BookingReference = reference,
                CustomerId       = request.CustomerId,
                ApartmentId      = request.ApartmentId,
                Room             = request.Room,
                RoomType         = request.RoomType,
                BoardType        = request.BoardType,
                Occupancy        = request.Occupancy,
                CheckIn          = request.CheckIn,
                CheckOut         = request.CheckOut,
                Adults           = request.Adults,
                Children         = request.Children,
                ChildrenAges     = request.ChildrenAges,
                TotalPrice       = finalPrice,
                Status           = BookingStatus.Pending,
                SpecialRequests  = request.SpecialRequests,
                CreatedAt        = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Booking created: {Reference} for Customer {CustomerId}, Room {Room}",
                reference, request.CustomerId, request.Room);

            return await GetBookingByIdAsync(booking.Id);
        }

        // ===== PUBLIC: date-aware room availability =====
        // Called by BookingsController GET api/bookings/rooms/available?type=Studio&checkIn=...&checkOut=...
        // This is what Booking.razor calls so the guest can pick their specific room.

        public async Task<List<RoomAvailabilityDto>> GetAvailableRoomsForDatesAsync(
            string type,
            DateTime checkIn,
            DateTime checkOut)
        {
            // Accept both enum names ("Studio") and display strings ("studio", "one-bedroom")
            RoomTypeEnum roomTypeEnum;

            if (!Enum.TryParse<RoomTypeEnum>(type, true, out roomTypeEnum))
            {
                roomTypeEnum = type.ToLower().Replace(" ", "").Replace("-", "") switch
                {
                    "studio"      => RoomTypeEnum.Studio,
                    "onebedroom"  => RoomTypeEnum.OneBedroom,
                    "twobedroom"  => RoomTypeEnum.TwoBedroom,
                    _ => throw new ArgumentException($"Invalid room type: {type}")
                };
            }

            return await GetAvailableRoomsForBookingAsync(roomTypeEnum, checkIn, checkOut);
        }

        // Keep old method for backward compatibility (used internally)
        public async Task<List<RoomAvailabilityDto>> GetAvailableRoomsAsync(string type)
        {
            return await GetAvailableRoomsForDatesAsync(type, DateTime.Today, DateTime.Today.AddDays(1));
        }

        // ===== OTHER PUBLIC METHODS =====

        public async Task<BookingResponseDto> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                throw new KeyNotFoundException($"Booking with ID {id} not found");

            return MapToDto(booking);
        }

        public async Task<BookingResponseDto> GetBookingByReferenceAsync(string reference)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .FirstOrDefaultAsync(b => b.BookingReference == reference);

            if (booking == null)
                throw new KeyNotFoundException($"Booking with reference {reference} not found");

            return MapToDto(booking);
        }

        public async Task<List<BookingResponseDto>> GetAllBookingsAsync()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return bookings.Select(MapToDto).ToList();
        }

        public async Task<List<BookingResponseDto>> GetBookingsByStatusAsync(string status)
        {
            if (!Enum.TryParse<BookingStatus>(status, true, out var parsedStatus))
                throw new ArgumentException($"Invalid booking status: {status}");

            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .Where(b => b.Status == parsedStatus)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return bookings.Select(MapToDto).ToList();
        }

        public async Task<List<BookingResponseDto>> GetBookingsByCustomerAsync(int customerId)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .Where(b => b.CustomerId == customerId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return bookings.Select(MapToDto).ToList();
        }

        public async Task UpdateBookingStatusAsync(int bookingId, BookingStatus status)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
                throw new KeyNotFoundException($"Booking with ID {bookingId} not found");

            booking.Status    = status;
            booking.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Booking {Id} status updated to {Status}", bookingId, status);
        }

        public async Task CancelBookingAsync(int bookingId)
        {
            await UpdateBookingStatusAsync(bookingId, BookingStatus.Cancelled);
        }

        // ===== PRIVATE HELPERS =====

        private async Task<List<RoomAvailabilityDto>> GetAvailableRoomsForBookingAsync(
            RoomTypeEnum roomType,
            DateTime checkIn,
            DateTime checkOut)
        {
            var apartmentTypeString = roomType switch
            {
                RoomTypeEnum.Studio     => "studio",
                RoomTypeEnum.OneBedroom => "one-bedroom",
                RoomTypeEnum.TwoBedroom => "two-bedroom",
                _ => throw new ArgumentException($"Invalid room type: {roomType}")
            };

            var apartment = await _context.Apartments
                .FirstOrDefaultAsync(a => a.Type == apartmentTypeString);

            if (apartment == null)
                return new List<RoomAvailabilityDto>();

            var allRooms = await _context.Rooms
                .Where(r => r.ApartmentId == apartment.Id && r.IsAvailable)
                .ToListAsync();

            var bookedRoomNumbers = await _context.Bookings
                .Where(b => b.ApartmentId == apartment.Id &&
                           (b.Status == BookingStatus.Confirmed ||
                            b.Status == BookingStatus.Pending ||
                            b.Status == BookingStatus.CheckedIn) &&
                            b.CheckIn < checkOut &&
                            b.CheckOut > checkIn)
                .Select(b => b.Room)
                .Distinct()
                .ToListAsync();

            return allRooms
                .Where(r => !bookedRoomNumbers.Contains(r.Number))
                .OrderBy(r => r.Floor)
                .ThenBy(r => r.Number)
                .Select(r => new RoomAvailabilityDto
                {
                    RoomNumber  = r.Number,
                    Type        = r.Type,
                    Floor       = r.Floor,
                    IsAvailable = true
                })
                .ToList();
        }

        private async Task<bool> IsRoomAvailableAsync(
            string roomNumber, DateTime checkIn, DateTime checkOut)
        {
            var hasConflict = await _context.Bookings
                .AnyAsync(b => b.Room == roomNumber &&
                              (b.Status == BookingStatus.Confirmed ||
                               b.Status == BookingStatus.Pending ||
                               b.Status == BookingStatus.CheckedIn) &&
                               b.CheckIn < checkOut &&
                               b.CheckOut > checkIn);

            return !hasConflict;
        }

        private BookingResponseDto MapToDto(Booking booking)
        {
            return new BookingResponseDto
            {
                Id               = booking.Id,
                BookingReference = booking.BookingReference,
                CustomerId       = booking.CustomerId,
                Customer         = booking.Customer != null
                    ? $"{booking.Customer.FirstName} {booking.Customer.LastName}"
                    : "",
                Email            = booking.Customer?.Email ?? "",
                Phone            = booking.Customer?.Phone ?? "",
                ApartmentId      = booking.ApartmentId,
                ApartmentName    = booking.Apartment?.Name ?? "",
                ApartmentType    = booking.Apartment?.Type ?? "",
                Room             = booking.Room,
                RoomType         = booking.RoomType.ToString(),
                BoardType        = booking.BoardType,
                Occupancy        = booking.Occupancy,
                CheckIn          = booking.CheckIn,
                CheckOut         = booking.CheckOut,
                Adults           = booking.Adults,
                Children         = booking.Children,
                ChildrenAges     = booking.ChildrenAges,
                TotalPrice       = booking.TotalPrice,
                Status           = booking.Status.ToString(),
                SpecialRequests  = booking.SpecialRequests,
                CreatedAt        = booking.CreatedAt,
                UpdatedAt        = booking.UpdatedAt
            };
        }

        private string GenerateBookingReference() =>
            Guid.NewGuid().ToString("N")[..8].ToUpper();

        private RoomTypeEnum MapApartmentTypeToEnum(string apartmentType) =>
            apartmentType.ToLower() switch
            {
                "studio"      => RoomTypeEnum.Studio,
                "one-bedroom" => RoomTypeEnum.OneBedroom,
                "two-bedroom" => RoomTypeEnum.TwoBedroom,
                _ => throw new ArgumentException($"Unknown apartment type: {apartmentType}")
            };

        private int GetMaxGuestsForOccupancy(string occupancy) =>
            occupancy switch
            {
                "Single"    => 1,
                "Double"    => 2,
                "Quadruple" => 4,
                _ => throw new ArgumentException($"Invalid occupancy type: {occupancy}")
            };

        private int GetMaxCapacityForRoomType(RoomTypeEnum roomType) =>
            roomType switch
            {
                RoomTypeEnum.Studio     => 2,
                RoomTypeEnum.OneBedroom => 2,
                RoomTypeEnum.TwoBedroom => 4,
                _ => throw new ArgumentException($"Invalid room type: {roomType}")
            };
    }
}