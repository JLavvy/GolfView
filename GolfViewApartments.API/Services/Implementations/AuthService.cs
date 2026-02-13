using GolfViewApartments.API.Data;
using GolfViewApartments.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace GolfViewApartments.API.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _db;
    private readonly IPasswordService _passwordService;
    private readonly IConfiguration _configuration;

public AuthService(
    ApplicationDbContext db,
    IPasswordService passwordService,
    IConfiguration configuration)
{
    _db = db;
    _passwordService = passwordService;
    _configuration = configuration;
}



public async Task<AuthResult?> LoginAsync(string email, string password)
{
    if (string.IsNullOrWhiteSpace(email) ||
        string.IsNullOrWhiteSpace(password))
        return null;

    var admin = await _db.Admins
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Email == email);

    if (admin == null)
        return null;

    if (!_passwordService.Verify(password, admin.PasswordHash))
        return null;

    // ✅ CLAIMS
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
        new Claim(ClaimTypes.Email, admin.Email),
        new Claim(ClaimTypes.Role, "Admin")
    };

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
    );

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(
            int.Parse(_configuration["Jwt:ExpiryMinutes"]!)
        ),
        signingCredentials: creds
    );

    return new AuthResult(
        new JwtSecurityTokenHandler().WriteToken(token),
        token.ValidTo
    );
}

}
