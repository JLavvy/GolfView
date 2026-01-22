using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public PhotosController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: api/photos
        [HttpGet]
        public async Task<ActionResult<List<Photo>>> GetPhotos([FromQuery] string? category = null)
        {
            var query = _context.Photos.AsQueryable();
            
            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                query = query.Where(p => p.Category == category);
            }
            
            return await query.OrderBy(p => p.DisplayOrder).ToListAsync();
        }
        
        // POST: api/photos
        [HttpPost]
        public async Task<ActionResult<Photo>> CreatePhoto(Photo photo)
        {
            photo.UploadedAt = DateTime.UtcNow;
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, photo);
        }
        
        // GET: api/photos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            return photo == null ? NotFound() : Ok(photo);
        }
        
        // DELETE: api/photos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null) return NotFound();
            
            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}