using Auth.Domain;
using System;
using System.Threading.Tasks;

namespace Auth.Application.Ports.Services
{
    public interface IAuthTokenService
    {
        Task<string> GenerateToken(User user);
        Task<string> GenerateRefreshToken(int size);
        Task<Guid> GetUserIdFromToken(string token);
        Task<bool> IsTokenValid(string accessToken, bool validateLifeTime);
    }
}
