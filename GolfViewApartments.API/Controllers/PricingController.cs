using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfViewApartments.API.Data;
using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public PricingController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: api/pricing/amenities
        [HttpGet("amenities")]
        public async Task<ActionResult<List<AmenityPricing>>> GetAmenityPricing()
        {
            return await _context.AmenityPricing.ToListAsync();
        }
        
        // PUT: api/pricing/amenities
        [HttpPut("amenities")]
        public async Task<IActionResult> UpdateAmenityPricing([FromBody] List<AmenityPricing> amenities)
        {
            foreach (var amenity in amenities)
            {
                var existing = await _context.AmenityPricing.FindAsync(amenity.Id);
                if (existing != null)
                {
                    existing.Price = amenity.Price;
                    existing.UpdatedAt = DateTime.UtcNow;
                }
            }
            
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}