namespace GolfViewApartments.API.Services.Interfaces;

public interface IPasswordService
{
    bool Verify(string password, string hash);
    string Hash(string password);
}
