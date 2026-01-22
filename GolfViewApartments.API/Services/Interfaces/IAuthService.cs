namespace GolfViewApartments.API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResult?> LoginAsync(string email, string password);
}

public record AuthResult(string Token, DateTime ExpiresAt);
