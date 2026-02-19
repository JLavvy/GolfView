using GolfViewApartments.API.Data;
using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Services.Interfaces;
using GolfViewApartments.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AvailabilityService> _logger;

        public AvailabilityService(ApplicationDbContext context, ILogger<AvailabilityService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<AvailableApartmentDto>> GetAvailableApartmentsAsync(
            DateTime checkIn,
            DateTime checkOut,
            int guests)
        {
            // Load all apartments with their rooms
            var apartments = await _context.Apartments
                .Include(a => a.Rooms)
                .ToListAsync();

            // Get all room numbers that are conflicted for these dates
            // (Pending, Confirmed, or CheckedIn bookings that overlap)
            var bookedRoomNumbers = await _context.Bookings
                .Where(b =>
                    (b.Status == BookingStatus.Confirmed ||
                     b.Status == BookingStatus.Pending ||
                     b.Status == BookingStatus.CheckedIn)
                    && b.CheckIn < checkOut
                    && b.CheckOut > checkIn)
                .Select(b => b.Room)
                .Distinct()
                .ToListAsync();

            var result = new List<AvailableApartmentDto>();

            foreach (var apartment in apartments)
            {
                // Filter: only apartments that can fit the guest count
                if (apartment.MaxGuests < guests)
                    continue;

                // Count rooms in this apartment that are not booked
                var availableRooms = apartment.Rooms
                    .Where(r => r.IsAvailable && !bookedRoomNumbers.Contains(r.Number))
                    .ToList();

                if (!availableRooms.Any())
                    continue;

                result.Add(new AvailableApartmentDto
                {
                    Id = apartment.ApartmentId,          // e.g. "studio-apartment"
                    Name = apartment.Name,
                    Description = GetDescription(apartment.Type),
                    Image = GetImage(apartment.Type),
                    Bedrooms = GetBedrooms(apartment.Type),
                    MaxGuests = apartment.MaxGuests,
                    Size = apartment.Size,
                    AvailableRooms = availableRooms.Count
                });
            }

            return result;
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null || !room.IsAvailable)
                return false;

            var hasConflict = await _context.Bookings
                .AnyAsync(b =>
                    b.Room == room.Number
                    && (b.Status == BookingStatus.Confirmed ||
                        b.Status == BookingStatus.Pending ||
                        b.Status == BookingStatus.CheckedIn)
                    && b.CheckIn < checkOut
                    && b.CheckOut > checkIn);

            return !hasConflict;
        }

        public async Task<List<Room>> GetAvailableRoomsAsync(
            int apartmentId,
            DateTime checkIn,
            DateTime checkOut)
        {
            var allRooms = await _context.Rooms
                .Where(r => r.ApartmentId == apartmentId && r.IsAvailable)
                .ToListAsync();

            var bookedRoomNumbers = await _context.Bookings
                .Where(b =>
                    b.ApartmentId == apartmentId
                    && (b.Status == BookingStatus.Confirmed ||
                        b.Status == BookingStatus.Pending ||
                        b.Status == BookingStatus.CheckedIn)
                    && b.CheckIn < checkOut
                    && b.CheckOut > checkIn)
                .Select(b => b.Room)
                .Distinct()
                .ToListAsync();

            return allRooms
                .Where(r => !bookedRoomNumbers.Contains(r.Number))
                .ToList();
        }

        // ===== PRIVATE HELPERS =====
        // These fill in the display fields that the frontend expects.
        // Update descriptions/images to match your actual content.

        private string GetDescription(string type) => type switch
        {
            "studio" => "A cozy, self-contained studio apartment ideal for solo travelers or couples. Features a fully equipped kitchenette, modern bathroom, and a comfortable living/sleeping area.",
            "one-bedroom" => "A spacious one-bedroom apartment with a separate living area, fully equipped kitchen, and modern bathroom. Perfect for couples or business travelers.",
            "two-bedroom" => "A generous two-bedroom apartment with a large living area, full kitchen, and two modern bathrooms. Ideal for families or groups of up to four guests.",
            _ => ""
        };

        private string GetImage(string type) => type switch
        {
            "studio" => "/images/studio.jpg",
            "one-bedroom" => "/images/one-bedroom.jpg",
            "two-bedroom" => "/images/two-bedroom.jpg",
            _ => "/images/default.jpg"
        };

        private string GetBedrooms(string type) => type switch
        {
            "studio" => "Studio",
            "one-bedroom" => "1",
            "two-bedroom" => "2",
            _ => "—"
        };
    }
}