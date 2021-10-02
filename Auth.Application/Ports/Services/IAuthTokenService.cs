using Auth.Domain;
using System;
using System.Threading.Tasks;

namespace Auth.Application.Ports.Services
{
    public interface IAuthTokenService
    {
        Task<string> GenerateToken(User user);
        Task<string> GenerateRefreshToken();
        Task<Guid> GetUserIdFromToken(string token);
        Task<int> GetRefreshTokenLifetimeInMinutes();
        Task<bool> IsTokenValid(string accessToken, bool validateLifeTime);
    }
}
