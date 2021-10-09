using Auth.Application.UseCases.RefreshToken.Request;
using Auth.Application.UseCases.RefreshToken.Response;

namespace Auth.Application.UseCases.RefreshToken
{
    public interface IRefreshTokenUseCase : IUseCase<RefreshTokenRequest, RefreshTokenResponse>
    {
    }
}
