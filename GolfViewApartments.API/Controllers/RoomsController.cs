using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/rooms
        [HttpGet]
        public async Task<ActionResult<List<Room>>> GetAllRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        // GET: api/rooms/type/Studio
        [HttpGet("type/{type}")]
        public async Task<ActionResult<List<Room>>> GetRoomsByType(string type)
        {
            var rooms = await _context.Rooms
                .Where(r => r.Type == type)
                .ToListAsync();
            return Ok(rooms);
        }

        // PUT: api/rooms/5/toggle-availability
        [HttpPut("{id}/toggle-availability")]
        public async Task<IActionResult> ToggleRoomAvailability(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();

            room.IsAvailable = !room.IsAvailable;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/rooms/bulk-update
        [HttpPut("bulk-update")]
        public async Task<IActionResult> BulkUpdateRooms([FromBody] List<RoomUpdateDto> updates)
        {
            foreach (var update in updates)
            {
                var room = await _context.Rooms.FindAsync(update.Id);
                if (room != null)
                {
                    room.IsAvailable = update.IsAvailable;
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class RoomUpdateDto
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
    }
}