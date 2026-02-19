using GolfViewApartments.API.Common.Responses;
using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GolfViewApartments.API.Controllers
{
    /// <summary>
    /// Manages apartment availability and search.
    /// Called by SearchResults.razor via: api/availability/search
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        private readonly ILogger<AvailabilityController> _logger;

        public AvailabilityController(
            IAvailabilityService availabilityService,
            ILogger<AvailabilityController> logger)
        {
            _availabilityService = availabilityService;
            _logger = logger;
        }

        /// <summary>
        /// Search for available apartments for the given dates and guest count.
        /// Called by SearchResults.razor.
        /// GET api/availability/search?checkIn=2025-06-01&checkOut=2025-06-05&guests=2
        /// </summary>
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<AvailableApartmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AvailableApartmentDto>>> Search(
            [FromQuery] DateTime checkIn,
            [FromQuery] DateTime checkOut,
            [FromQuery] int guests)
        {
            if (checkIn == default || checkOut == default)
                return BadRequest("Check-in and check-out dates are required.");

            if (checkOut <= checkIn)
                return BadRequest("Check-out date must be after check-in date.");

            if (guests < 1)
                return BadRequest("At least 1 guest is required.");

            try
            {
                var available = await _availabilityService.GetAvailableApartmentsAsync(
                    checkIn, checkOut, guests);

                return Ok(available);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching availability for {CheckIn} to {CheckOut}", checkIn, checkOut);
                return StatusCode(500, "An error occurred while searching for available apartments.");
            }
        }
    }
}