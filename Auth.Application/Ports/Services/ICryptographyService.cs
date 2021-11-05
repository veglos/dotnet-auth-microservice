namespace Auth.Application.Ports.Services
{
    public interface ICryptographyService
    {
        string GenerateSalt();

        string HashPassword(string password, string salt);
    }
}
