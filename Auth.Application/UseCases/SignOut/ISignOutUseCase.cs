using Auth.Application.UseCases.SignOut.Request;
using Auth.Application.UseCases.SignOut.Response;

namespace Auth.Application.UseCases.SignOut
{
    public interface ISignOutUseCase : IUseCase<SignOutRequest, SignOutResponse>
    {

    }
}
