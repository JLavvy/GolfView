using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfViewApartments.API.Data;
using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Models;

namespace GolfViewApartments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/contact/messages
        [HttpPost("messages")]

        public async Task<IActionResult> SubmitContactMessage(ContactMessageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var message = new ContactMessage
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Subject = dto.Subject,
                Message = dto.Message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Message sent successfully" });
        }


        // GET: api/contact/messages
        [HttpGet("messages")]
        public async Task<ActionResult<List<ContactMessage>>> GetAllMessages()
        {
            return await _context.ContactMessages
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        // GET: api/contact/info
        [HttpGet("info")]
        public async Task<ActionResult<ContactInfo>> GetContactInfo()
        {
            var info = await _context.ContactInfo.FirstOrDefaultAsync();
            return info == null ? NotFound() : Ok(info);
        }

        // PUT: api/contact/info
        [HttpPut("info")]
        public async Task<IActionResult> UpdateContactInfo(ContactInfoDto dto)
        {
            var info = await _context.ContactInfo.FirstOrDefaultAsync();

            if (info == null)
            {
                info = new ContactInfo { Id = 1 };
                _context.ContactInfo.Add(info);
            }

            info.Address = dto.Address;
            info.Phone = dto.Phone;
            info.Email = dto.Email;
            info.WhatsApp = dto.WhatsApp;
            info.Website = dto.Website;
            info.Description = dto.Description;
            info.FacebookUrl = dto.FacebookUrl;
            info.InstagramUrl = dto.InstagramUrl;
            info.TwitterUrl = dto.TwitterUrl;
            info.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}