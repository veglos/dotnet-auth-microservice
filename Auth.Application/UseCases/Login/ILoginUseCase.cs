using Auth.Application.UseCases.Login.Request;
using Auth.Application.UseCases.Login.Response;

namespace Auth.Application.UseCases.Login
{
    public interface ILoginUseCase : IUseCase<LoginRequest, LoginResponse>
    {
    }
}
