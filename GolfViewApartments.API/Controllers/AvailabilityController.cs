// using GolfViewApartments.API.Common.Responses;
// using GolfViewApartments.API.DTOs;
// using GolfViewApartments.API.Models;
// using GolfViewApartments.API.Services.Interfaces;
// using GolfViewApartments.API.Data;
// using Microsoft.AspNetCore.Mvc;
// namespace GolfViewApartments.API.Controllers
// {
//     /// <summary>
//     /// Manages apartment availability and search
//     /// </summary>

// [ApiController]
// [Route("api/[controller]")]
// public class AvailabilityController : ControllerBase
// {
//     private readonly IAvailabilityService _availabilityService;
    
//     [HttpGet("search")]
//     public async Task<ActionResult<List<AvailableApartmentDto>>> Search(
//         [FromQuery] DateTime checkIn,
//         [FromQuery] DateTime checkOut,
//         [FromQuery] int adults,
//         [FromQuery] int children
//     )
//     {
//         // Returns only apartments with available rooms
//     }
// }
// }