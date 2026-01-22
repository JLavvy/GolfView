using GolfViewApartments.API.Data;
using GolfViewApartments.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolfViewApartments.API.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _db;
    private readonly IPasswordService _passwordService;

    public AuthService(
        ApplicationDbContext db,
        IPasswordService passwordService)
    {
        _db = db;
        _passwordService = passwordService;
    }

    public async Task<AuthResult?> LoginAsync(string email, string password)
    {
        // ✅ GUARD CLAUSE (prevents null crashes)
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

        // ⚠ TEMP TOKEN (JWT comes later)
        var token = Guid.NewGuid().ToString();

        return new AuthResult(
            token,
            DateTime.UtcNow.AddHours(2)
        );
    }
}
