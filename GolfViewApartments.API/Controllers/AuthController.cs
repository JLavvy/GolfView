using GolfViewApartments.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GolfViewApartments.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

   [HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginRequest request)
{
    if (string.IsNullOrWhiteSpace(request.Email) ||
        string.IsNullOrWhiteSpace(request.Password))
    {
        return BadRequest("Email and password are required");
    }

    var result = await _authService.LoginAsync(
        request.Email,
        request.Password);

    if (result == null)
        return Unauthorized();

    return Ok(result);
}


    public record LoginRequest(string Email, string Password);
}
