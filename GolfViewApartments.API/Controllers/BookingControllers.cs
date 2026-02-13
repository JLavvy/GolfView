using GolfViewApartments.API.Common.Responses;
using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Services.Interfaces;
using GolfViewApartments.API.Data;
using Microsoft.AspNetCore.Mvc;
using GolfViewApartments.Shared.Enums;


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
            try
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
                    ApiResponse<BookingResponseDto>.SuccessResponse(
                        booking,
                        $"Booking created successfully. Reference: {booking.BookingReference}"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<BookingResponseDto>.FailureResponse(
                    "Invalid request", ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking");
                return StatusCode(500, ApiResponse<BookingResponseDto>.FailureResponse(
                    "An error occurred while creating the booking"));
            }
        }

        /// <summary>
        /// Get booking by ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<BookingResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<BookingResponseDto>>> GetBooking(int id)
        {
            try
            {
                var booking = await _bookingService.GetBookingByIdAsync(id);
                return Ok(ApiResponse<BookingResponseDto>.SuccessResponse(
                    booking,
                    "Booking retrieved successfully"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<BookingResponseDto>.FailureResponse(
                    "Booking not found", ex.Message));
            }
        }

        /// <summary>
        /// Get booking by reference code
        /// </summary>
        [HttpGet("reference/{reference}")]
        [ProducesResponseType(typeof(ApiResponse<BookingResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<BookingResponseDto>>> GetBookingByReference(string reference)
        {
            try
            {
                var booking = await _bookingService.GetBookingByReferenceAsync(reference);
                return Ok(ApiResponse<BookingResponseDto>.SuccessResponse(
                    booking,
                    "Booking retrieved successfully"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<BookingResponseDto>.FailureResponse(
                    "Booking not found", ex.Message));
            }
        }

        /// <summary>
        /// Get all bookings, optionally filtered by status
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<BookingResponseDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<BookingResponseDto>>>> GetAllBookings(
            [FromQuery] string? status = null)
        {
            try
            {
                var bookings = string.IsNullOrEmpty(status)
                    ? await _bookingService.GetAllBookingsAsync()
                    : await _bookingService.GetBookingsByStatusAsync(status);

                return Ok(ApiResponse<List<BookingResponseDto>>.SuccessResponse(
                    bookings,
                    $"Retrieved {bookings.Count} booking(s)"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<List<BookingResponseDto>>.FailureResponse(
                    "Invalid status", ex.Message));
            }
        }

        /// <summary>
        /// Get bookings by customer ID
        /// </summary>
        [HttpGet("customer/{customerId:int}")]
        [ProducesResponseType(typeof(ApiResponse<List<BookingResponseDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<BookingResponseDto>>>> GetCustomerBookings(int customerId)
        {
            var bookings = await _bookingService.GetBookingsByCustomerAsync(customerId);
            return Ok(ApiResponse<List<BookingResponseDto>>.SuccessResponse(
                bookings,
                $"Retrieved {bookings.Count} booking(s) for customer"));
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
            try
            {
                if (!Enum.TryParse<BookingStatus>(request.Status, true, out var status))
                {
                    return BadRequest(ApiResponse.FailureResponse(
                        "Invalid status",
                        $"Status must be one of: {string.Join(", ", Enum.GetNames<BookingStatus>())}"));
                }

                await _bookingService.UpdateBookingStatusAsync(id, status);

                return Ok(ApiResponse.SuccessResponse(
                    $"Booking status updated to {status}"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse.FailureResponse(
                    "Booking not found", ex.Message));
            }
        }

        /// <summary>
        /// Cancel a booking
        /// </summary>
        [HttpPut("{id:int}/cancel")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> CancelBooking(int id)
        {
            try
            {
                await _bookingService.CancelBookingAsync(id);
                return Ok(ApiResponse.SuccessResponse("Booking cancelled successfully"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse.FailureResponse(
                    "Booking not found", ex.Message));
            }
        }

        /// <summary>
        /// Get available rooms by apartment type
        /// </summary>
        [HttpGet("rooms/available")]
        [ProducesResponseType(typeof(ApiResponse<List<RoomAvailabilityDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<RoomAvailabilityDto>>>> GetAvailableRooms(
            [FromQuery] string type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type))
                {
                    return BadRequest(ApiResponse<List<RoomAvailabilityDto>>.FailureResponse(
                        "Apartment type is required"));
                }

                var rooms = await _bookingService.GetAvailableRoomsAsync(type);
                return Ok(ApiResponse<List<RoomAvailabilityDto>>.SuccessResponse(
                    rooms,
                    $"Found {rooms.Count} available room(s)"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving available rooms for type {Type}", type);
                return StatusCode(500, ApiResponse<List<RoomAvailabilityDto>>.FailureResponse(
                    "An error occurred while retrieving available rooms"));
            }
        }
    }
}