using Auth.Application.UseCases.Login.Request;
using Auth.Application.UseCases.Login.Response;
using System.Threading.Tasks;

namespace Auth.Application.UseCases.Login
{
    public interface ILoginUseCase
    {
        Task<LoginResponse> Execute(LoginRequest request);
    }
}
