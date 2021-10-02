using Auth.Application.UseCases.RefreshToken.Request;
using Auth.Application.UseCases.RefreshToken.Response;
using System.Threading.Tasks;

namespace Auth.Application.UseCases.RefreshToken
{
    public interface IRefreshTokenUseCase
    {
        Task<RefreshTokenResponse> Execute(RefreshTokenRequest request);
    }
}
