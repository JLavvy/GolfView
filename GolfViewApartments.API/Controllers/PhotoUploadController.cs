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

      // ================= UPLOAD FILE =================
[HttpPost("upload")]
[Consumes("multipart/form-data")]
public async Task<ActionResult<object>> UploadPhoto(
    [FromForm] IFormFile file,
    [FromForm] string category)
{
    if (file == null || file.Length == 0)
        return BadRequest("No file uploaded.");

    var imagesPath = Path.Combine(
        Directory.GetCurrentDirectory(), "wwwroot", "images");

    if (!Directory.Exists(imagesPath))
        Directory.CreateDirectory(imagesPath);

    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
    var fullPath = Path.Combine(imagesPath, fileName);

    await using var stream = new FileStream(fullPath, FileMode.Create);
    await file.CopyToAsync(stream);

    // CHANGED: Return full URL instead of just path
    var baseUrl = $"{Request.Scheme}://{Request.Host}";
    return Ok(new { url = $"{baseUrl}/images/{fileName}" });
}
        // ================= GET ALL =================
        [HttpGet]
        public async Task<ActionResult<List<Photo>>> GetPhotos(
            [FromQuery] string? category = null)
        {
            var query = _context.Photos.AsQueryable();

            if (!string.IsNullOrEmpty(category) && category != "All")
                query = query.Where(p => p.Category == category);

            return await query
                .OrderBy(p => p.DisplayOrder)
                .ToListAsync();
        }

        // ================= CREATE METADATA =================
        [HttpPost]
        public async Task<ActionResult<Photo>> CreatePhoto(Photo photo)
        {
            photo.UploadedAt = DateTime.UtcNow;
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return Ok(photo);
        }

        // ================= DELETE =================
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
