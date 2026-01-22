using GolfViewApartments.API.Common.Responses;
using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GolfViewApartments.API.Controllers
{
    /// <summary>
    /// Manages bookings and reservations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(
            IBookingService bookingService,
            ILogger<BookingsController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        /// <summary>
        /// Create a new booking
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<BookingResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<BookingResponseDto>>> CreateBooking(
            [FromBody] BookingRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<BookingResponseDto>.FailureResponse(
                    "Validation failed",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
            }

            var booking = await _bookingService.CreateBookingAsync(request);
            
            return CreatedAtAction(
                nameof(GetBooking),
                new { id = booking.Id },
                ApiResponse<BookingResponseDto>.SuccessResponse(booking, "Booking created successfully"));
        }

        /// <summary>
        /// Get booking by ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<BookingResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<BookingResponseDto>>> GetBooking(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            return Ok(ApiResponse<BookingResponseDto>.SuccessResponse(
                booking, 
                "Booking retrieved successfully"));
        }

        /// <summary>
        /// Get all bookings, optionally filtered by status
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<BookingResponseDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<BookingResponseDto>>>> GetAllBookings(
            [FromQuery] string? status = null)
        {
            var bookings = string.IsNullOrEmpty(status)
                ? await _bookingService.GetAllBookingsAsync()
                : await _bookingService.GetBookingsByStatusAsync(status);

            return Ok(ApiResponse<List<BookingResponseDto>>.SuccessResponse(
                bookings, 
                "Bookings retrieved successfully"));
        }

        /// <summary>
        /// Update booking status
        /// </summary>
        [HttpPut("{id:int}/status")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> UpdateBookingStatus(
            int id, 
            [FromBody] UpdateBookingStatusDto request)
        {
            if (!Enum.TryParse<BookingStatus>(request.Status, true, out var status))
            {
                return BadRequest(ApiResponse.FailureResponse(
                    "Invalid status", 
                    $"Status must be one of: {string.Join(", ", Enum.GetNames<BookingStatus>())}"));
            }

            await _bookingService.UpdateBookingStatusAsync(id, status);
            
            return Ok(ApiResponse.SuccessResponse("Booking status updated successfully"));
        }

        /// <summary>
        /// Cancel a booking
        /// </summary>
        [HttpPut("{id:int}/cancel")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> CancelBooking(int id)
        {
            await _bookingService.CancelBookingAsync(id);
            return Ok(ApiResponse.SuccessResponse("Booking cancelled successfully"));
        }

        /// <summary>
        /// Get available rooms by apartment type
        /// </summary>
        [HttpGet("rooms/available")]
        [ProducesResponseType(typeof(ApiResponse<List<RoomAvailabilityDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<RoomAvailabilityDto>>>> GetAvailableRooms(
            [FromQuery] string type)
        {
            var rooms = await _bookingService.GetAvailableRoomsAsync(type);
            return Ok(ApiResponse<List<RoomAvailabilityDto>>.SuccessResponse(
                rooms, 
                "Available rooms retrieved successfully"));
        }
    }

    /// <summary>
    /// DTO for updating booking status
    /// </summary>
    public class UpdateBookingStatusDto
    {
        public string Status { get; set; } = string.Empty;
    }
}