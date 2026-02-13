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