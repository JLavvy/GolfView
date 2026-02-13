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

        private async Task<int> GetOrCreateCustomerAsync(string firstName, string lastName, string email, string phone)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email);

            if (customer == null)
            {
                customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            return customer.Id;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto request)
        {
            // ===== VALIDATION PHASE =====
            
            // 1. Validate dates
            if (request.CheckOut <= request.CheckIn)
            {
                throw new ArgumentException("Check-out date must be after check-in date");
            }

            // 2. Get or create customer if CustomerId is 0
            if (request.CustomerId == 0)
            {
                var nameParts = request.Customer.Split(' ', 2);
                var firstName = nameParts.Length > 0 ? nameParts[0] : request.Customer;
                var lastName = nameParts.Length > 1 ? nameParts[1] : "";
                
                request.CustomerId = await GetOrCreateCustomerAsync(
                    firstName, 
                    lastName, 
                    request.Email, 
                    request.Phone);
            }

            // 3. Validate apartment exists OR find it by room type
            Apartment? apartment = null;
            
            if (request.ApartmentId > 0)
            {
                apartment = await _context.Apartments.FindAsync(request.ApartmentId);
            }
            else
            {
                // Find apartment by room type
                var apartmentType = request.RoomType switch
                {
                    RoomTypeEnum.Studio => "studio",
                    RoomTypeEnum.OneBedroom => "one-bedroom",
                    RoomTypeEnum.TwoBedroom => "two-bedroom",
                    _ => throw new ArgumentException($"Invalid room type: {request.RoomType}")
                };
                
                apartment = await _context.Apartments
                    .FirstOrDefaultAsync(a => a.Type == apartmentType);
                
                if (apartment != null)
                {
                    request.ApartmentId = apartment.Id;
                }
            }
            
            if (apartment == null)
            {
                throw new ArgumentException($"Could not find apartment for selected room type");
            }

            // 4. Validate RoomType matches ApartmentId
            var expectedRoomType = MapApartmentTypeToEnum(apartment.Type);
            if (request.RoomType != expectedRoomType)
            {
                throw new ArgumentException(
                    $"Room type mismatch: Selected apartment is '{apartment.Type}' " +
                    $"but room type '{request.RoomType}' was specified. " +
                    $"Expected room type: '{expectedRoomType}'");
            }

            // 5. Assign room if not provided
            if (string.IsNullOrEmpty(request.Room))
            {
                var availableRooms = await GetAvailableRoomsForBookingAsync(
                    request.RoomType,
                    request.CheckIn,
                    request.CheckOut);

                if (!availableRooms.Any())
                {
                    throw new ArgumentException(
                        $"No {request.RoomType} rooms are available for the selected dates");
                }

                request.Room = availableRooms.First().RoomNumber;
            }

            // 6. Validate guest count against occupancy
            var totalGuests = request.Adults + request.Children;
            var maxGuestsForOccupancy = GetMaxGuestsForOccupancy(request.Occupancy);
            
            if (totalGuests > maxGuestsForOccupancy)
            {
                throw new ArgumentException(
                    $"{request.Occupancy} occupancy supports maximum {maxGuestsForOccupancy} guests. " +
                    $"You have {totalGuests} guests (Adults: {request.Adults}, Children: {request.Children})");
            }

            // 7. Validate guest count against room capacity
            var maxCapacityForRoomType = GetMaxCapacityForRoomType(request.RoomType);
            if (totalGuests > maxCapacityForRoomType)
            {
                throw new ArgumentException(
                    $"{request.RoomType} room type supports maximum {maxCapacityForRoomType} guests. " +
                    $"You have {totalGuests} guests");
            }

            // 8. Validate room availability
            var isRoomAvailable = await IsRoomAvailableAsync(
                request.Room,
                request.CheckIn,
                request.CheckOut);

            if (!isRoomAvailable)
            {
                throw new ArgumentException(
                    $"Room {request.Room} is not available for the selected dates");
            }

            // 9. Validate RoomType + BoardType exists in RoomRate table
            var rateExists = await _pricingService.RateExistsAsync(
                request.RoomType, 
                request.BoardType);

            if (!rateExists)
            {
                var availableBoardTypes = await _pricingService.GetAvailableBoardTypesAsync(request.RoomType);
                throw new ArgumentException(
                    $"Rate not found for {request.RoomType} with board type '{request.BoardType}'. " +
                    $"Available board types: {string.Join(", ", availableBoardTypes)}");
            }

            // 10. Calculate price from RoomRate table
            var calculatedPrice = await _pricingService.CalculateBookingPriceAsync(
                request.RoomType,
                request.BoardType,
                request.Occupancy,
                request.CheckIn,
                request.CheckOut,
                request.Adults,
                request.Children);

            // 11. Validate provided price (if given) or use calculated price
            decimal finalPrice;
            if (request.TotalPrice.HasValue && request.TotalPrice.Value > 0)
            {
                // Client provided a price - validate it matches calculation
                var priceDifference = Math.Abs(request.TotalPrice.Value - calculatedPrice);
                if (priceDifference > 1.0m)
                {
                    _logger.LogWarning(
                        "Price mismatch for booking: Provided {ProvidedPrice}, Calculated {CalculatedPrice}",
                        request.TotalPrice.Value,
                        calculatedPrice);
                    
                    throw new ArgumentException(
                        $"Price mismatch: Provided price ${request.TotalPrice.Value:F2} " +
                        $"does not match calculated price ${calculatedPrice:F2}");
                }
                finalPrice = calculatedPrice; // Always use calculated price for consistency
            }
            else
            {
                // No price provided - use calculated price
                finalPrice = calculatedPrice;
            }

            // ===== CREATE BOOKING =====
            
            var reference = GenerateBookingReference();

            var booking = new Booking
            {
                BookingReference = reference,
                CustomerId = request.CustomerId,
                ApartmentId = request.ApartmentId,
                Room = request.Room,
                RoomType = request.RoomType,
                BoardType = request.BoardType,
                Occupancy = request.Occupancy,
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                Adults = request.Adults,
                Children = request.Children,
                ChildrenAges = request.ChildrenAges,
                TotalPrice = finalPrice,
                Status = BookingStatus.Pending,
                SpecialRequests = request.SpecialRequests,
                CreatedAt = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Booking created successfully: {Reference} for Customer {CustomerId}",
                reference,
                request.CustomerId);

            return await GetBookingByIdAsync(booking.Id);
        }

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

            booking.Status = status;
            booking.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Booking {BookingId} status updated to {Status}",
                bookingId,
                status);
        }

        public async Task CancelBookingAsync(int bookingId)
        {
            await UpdateBookingStatusAsync(bookingId, BookingStatus.Cancelled);
        }

        public async Task<List<RoomAvailabilityDto>> GetAvailableRoomsAsync(string type)
        {
            // Parse the room type - handle both enum names and display names
            RoomTypeEnum roomTypeEnum;
            
            if (!Enum.TryParse<RoomTypeEnum>(type, true, out roomTypeEnum))
            {
                // Try to map from display string to enum
                roomTypeEnum = type.ToLower().Replace(" ", "") switch
                {
                    "studio" => RoomTypeEnum.Studio,
                    "onebedroom" => RoomTypeEnum.OneBedroom,
                    "twobedroom" => RoomTypeEnum.TwoBedroom,
                    _ => throw new ArgumentException($"Invalid room type: {type}")
                };
            }

            // Get the apartment type string for database lookup
            var apartmentTypeString = roomTypeEnum switch
            {
                RoomTypeEnum.Studio => "studio",
                RoomTypeEnum.OneBedroom => "one-bedroom",
                RoomTypeEnum.TwoBedroom => "two-bedroom",
                _ => throw new ArgumentException($"Invalid room type enum: {roomTypeEnum}")
            };

            var apartment = await _context.Apartments
                .FirstOrDefaultAsync(a => a.Type == apartmentTypeString);

            if (apartment == null)
            {
                throw new ArgumentException($"Could not find apartment for room type: {type}");
            }

            var allRooms = await _context.Rooms
                .Where(r => r.ApartmentId == apartment.Id && r.IsAvailable)
                .ToListAsync();

            var bookedRoomNumbers = await _context.Bookings
                .Where(b => b.ApartmentId == apartment.Id && 
                           (b.Status == BookingStatus.Confirmed || 
                            b.Status == BookingStatus.Pending ||
                            b.Status == BookingStatus.CheckedIn))
                .Select(b => b.Room)
                .Distinct()
                .ToListAsync();

            var availableRooms = allRooms
                .Where(r => !bookedRoomNumbers.Contains(r.Number))
                .Select(r => new RoomAvailabilityDto
                {
                    RoomNumber = r.Number,
                    Type = r.Type,
                    Floor = r.Floor,
                    IsAvailable = true
                })
                .ToList();

            return availableRooms;
        }

        // ===== PRIVATE HELPER METHODS =====

        private async Task<List<RoomAvailabilityDto>> GetAvailableRoomsForBookingAsync(
            RoomTypeEnum roomType,
            DateTime checkIn,
            DateTime checkOut)
        {
            var apartmentTypeString = roomType switch
            {
                RoomTypeEnum.Studio => "studio",
                RoomTypeEnum.OneBedroom => "one-bedroom",
                RoomTypeEnum.TwoBedroom => "two-bedroom",
                _ => throw new ArgumentException($"Invalid room type: {roomType}")
            };

            var apartment = await _context.Apartments
                .FirstOrDefaultAsync(a => a.Type == apartmentTypeString);

            if (apartment == null)
            {
                return new List<RoomAvailabilityDto>();
            }

            var allRooms = await _context.Rooms
                .Where(r => r.ApartmentId == apartment.Id && r.IsAvailable)
                .ToListAsync();

            var bookedRoomNumbers = await _context.Bookings
                .Where(b => b.ApartmentId == apartment.Id && 
                           (b.Status == BookingStatus.Confirmed || 
                            b.Status == BookingStatus.Pending ||
                            b.Status == BookingStatus.CheckedIn)
                           && b.CheckIn < checkOut
                           && b.CheckOut > checkIn)
                .Select(b => b.Room)
                .Distinct()
                .ToListAsync();

            var availableRooms = allRooms
                .Where(r => !bookedRoomNumbers.Contains(r.Number))
                .Select(r => new RoomAvailabilityDto
                {
                    RoomNumber = r.Number,
                    Type = r.Type,
                    Floor = r.Floor,
                    IsAvailable = true
                })
                .ToList();

            return availableRooms;
        }

        private BookingResponseDto MapToDto(Booking booking)
        {
            return new BookingResponseDto
            {
                Id = booking.Id,
                BookingReference = booking.BookingReference,
                CustomerId = booking.CustomerId,
                Customer = booking.Customer != null 
                    ? $"{booking.Customer.FirstName} {booking.Customer.LastName}" 
                    : "",
                Email = booking.Customer?.Email ?? "",
                Phone = booking.Customer?.Phone ?? "",
                ApartmentId = booking.ApartmentId,
                ApartmentName = booking.Apartment?.Name ?? "",
                ApartmentType = booking.Apartment?.Type ?? "",
                Room = booking.Room,
                RoomType = booking.RoomType.ToString(),
                BoardType = booking.BoardType,
                Occupancy = booking.Occupancy,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                Adults = booking.Adults,
                Children = booking.Children,
                ChildrenAges = booking.ChildrenAges,
                TotalPrice = booking.TotalPrice,
                Status = booking.Status.ToString(),
                SpecialRequests = booking.SpecialRequests,
                CreatedAt = booking.CreatedAt,
                UpdatedAt = booking.UpdatedAt
            };
        }

        private string GenerateBookingReference()
        {
            return Guid.NewGuid().ToString("N")[..8].ToUpper();
        }

        private async Task<bool> IsRoomAvailableAsync(
            string roomNumber,
            DateTime checkIn,
            DateTime checkOut)
        {
            var hasConflict = await _context.Bookings
                .AnyAsync(b => b.Room == roomNumber
                            && (b.Status == BookingStatus.Confirmed || 
                                b.Status == BookingStatus.Pending ||
                                b.Status == BookingStatus.CheckedIn)
                            && b.CheckIn < checkOut
                            && b.CheckOut > checkIn);

            return !hasConflict;
        }

        private RoomTypeEnum MapApartmentTypeToEnum(string apartmentType)
        {
            return apartmentType.ToLower() switch
            {
                "studio" => RoomTypeEnum.Studio,
                "one-bedroom" => RoomTypeEnum.OneBedroom,
                "two-bedroom" => RoomTypeEnum.TwoBedroom,
                _ => throw new ArgumentException($"Unknown apartment type: {apartmentType}")
            };
        }

        private string GetApartmentTypeString(RoomTypeEnum roomType)
        {
            return roomType switch
            {
                RoomTypeEnum.Studio => "studio",
                RoomTypeEnum.OneBedroom => "one-bedroom",
                RoomTypeEnum.TwoBedroom => "two-bedroom",
                _ => throw new ArgumentException($"Invalid room type: {roomType}")
            };
        }

        private int GetMaxGuestsForOccupancy(string occupancy)
        {
            return occupancy switch
            {
                "Single" => 1,
                "Double" => 2,
                "Quadruple" => 4,
                _ => throw new ArgumentException($"Invalid occupancy type: {occupancy}")
            };
        }

        private int GetMaxCapacityForRoomType(RoomTypeEnum roomType)
        {
            return roomType switch
            {
                RoomTypeEnum.Studio => 2,
                RoomTypeEnum.OneBedroom => 2,
                RoomTypeEnum.TwoBedroom => 4,
                _ => throw new ArgumentException($"Invalid room type: {roomType}")
            };
        }
    }
}
