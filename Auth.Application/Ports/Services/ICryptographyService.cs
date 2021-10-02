namespace Auth.Application.Ports.Services
{
    public interface ICryptographyService
    {
        string GenerateRandomString(int size);

        string GenerateSalt();

        string HashPassword(string password, string salt);
    }
}
