using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using GolfViewApartments.API.Services.Interfaces;

namespace GolfViewApartments.API.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        // ----------------------------
        // CREATE BOOKING
        // ----------------------------
        public async Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto request)
        {
            var booking = new Booking
            {
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                Status = BookingStatus.Pending
            };

            await _bookingRepository.AddAsync(booking);

            return MapToResponseDto(booking);
        }

        // ----------------------------
        // GET BOOKING BY ID
        // ----------------------------
        public async Task<BookingResponseDto> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Booking not found");

            return MapToResponseDto(booking);
        }

        // ----------------------------
        // GET ALL BOOKINGS
        // ----------------------------
        public async Task<List<BookingResponseDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();

            return bookings
                .Select(MapToResponseDto)
                .ToList();
        }

        // ----------------------------
        // GET BOOKINGS BY STATUS
        // ----------------------------
        public async Task<List<BookingResponseDto>> GetBookingsByStatusAsync(string status)
        {
            if (!Enum.TryParse<BookingStatus>(status, true, out var parsedStatus))
                throw new ArgumentException("Invalid booking status");

            var bookings = await _bookingRepository.GetByStatusAsync(parsedStatus);

            return bookings
                .Select(MapToResponseDto)
                .ToList();
        }

        // ----------------------------
        // UPDATE BOOKING STATUS
        // ----------------------------
        public async Task UpdateBookingStatusAsync(int bookingId, BookingStatus status)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId)
                ?? throw new KeyNotFoundException("Booking not found");

            booking.Status = status;
            _bookingRepository.Update(booking);
        }

        // ----------------------------
        // CANCEL BOOKING
        // ----------------------------
        public async Task CancelBookingAsync(int bookingId)
        {
            await UpdateBookingStatusAsync(bookingId, BookingStatus.Cancelled);
        }

        // ----------------------------
        // GET AVAILABLE ROOMS (BY TYPE)
        // ----------------------------
        public async Task<List<RoomAvailabilityDto>> GetAvailableRoomsAsync(string apartmentType)
        {
            var rooms = await _roomRepository.GetAvailableRoomsAsync();

            var filteredRooms = rooms
                .Where(r => r.Type.Equals(apartmentType, StringComparison.OrdinalIgnoreCase));

            return filteredRooms
                .Select(r => new RoomAvailabilityDto
                {
                    RoomNumber = r.Number,
                    Type = r.Type
                })
                .ToList();
        }

        // ----------------------------
        // PRIVATE MAPPER
        // ----------------------------
        private static BookingResponseDto MapToResponseDto(Booking booking)
        {
            return new BookingResponseDto
            {
                Id = booking.Id,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                Status = booking.Status.ToString()
            };
        }
    }
}
