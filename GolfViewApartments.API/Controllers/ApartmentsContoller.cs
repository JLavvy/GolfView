using GolfViewApartments.API.Common.Responses;
using GolfViewApartments.API.DTOs.Apartment;
using GolfViewApartments.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GolfViewApartments.API.Controllers
{
    /// <summary>
    /// Manages apartment information and pricing
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly ILogger<ApartmentsController> _logger;

        public ApartmentsController(
            IApartmentService apartmentService,
            ILogger<ApartmentsController> logger)
        {
            _apartmentService = apartmentService;
            _logger = logger;
        }

        /// <summary>
        /// Get all apartments with their details
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ApartmentResponseDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<ApartmentResponseDto>>>> GetAll()
        {
            var apartments = await _apartmentService.GetAllApartmentsAsync();
            return Ok(ApiResponse<IEnumerable<ApartmentResponseDto>>.SuccessResponse(
                apartments, 
                "Apartments retrieved successfully"));
        }

        /// <summary>
        /// Get apartment by numeric ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<ApartmentResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<ApartmentResponseDto>>> GetById(int id)
        {
            var apartment = await _apartmentService.GetApartmentByIdAsync(id);
            return Ok(ApiResponse<ApartmentResponseDto>.SuccessResponse(
                apartment, 
                "Apartment retrieved successfully"));
        }

        /// <summary>
        /// Get apartment by apartment identifier (e.g., "studio-apartment")
        /// </summary>
        [HttpGet("by-identifier/{apartmentId}")]
        [ProducesResponseType(typeof(ApiResponse<ApartmentResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<ApartmentResponseDto>>> GetByApartmentId(string apartmentId)
        {
            var apartment = await _apartmentService.GetApartmentByApartmentIdAsync(apartmentId);
            return Ok(ApiResponse<ApartmentResponseDto>.SuccessResponse(
                apartment, 
                "Apartment retrieved successfully"));
        }

        /// <summary>
        /// Get apartment summaries (lightweight view)
        /// </summary>
        [HttpGet("summaries")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ApartmentSummaryDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<ApartmentSummaryDto>>>> GetSummaries()
        {
            var summaries = await _apartmentService.GetApartmentSummariesAsync();
            return Ok(ApiResponse<IEnumerable<ApartmentSummaryDto>>.SuccessResponse(
                summaries, 
                "Apartment summaries retrieved successfully"));
        }

        /// <summary>
        /// Update apartment pricing
        /// </summary>
        [HttpPut("{id:int}/pricing")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> UpdatePricing(
            int id, 
            [FromBody] UpdateApartmentPricingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.FailureResponse(
                    "Validation failed",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
            }

            await _apartmentService.UpdateApartmentPricingAsync(id, dto);
            
            return Ok(ApiResponse.SuccessResponse("Apartment pricing updated successfully"));
        }
    }
}