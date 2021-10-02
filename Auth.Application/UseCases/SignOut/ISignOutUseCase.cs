using Auth.Application.UseCases.SignOut.Request;
using Auth.Application.UseCases.SignOut.Response;
using System.Threading.Tasks;

namespace Auth.Application.UseCases.SignOut
{
    public interface ISignOutUseCase
    {
        public Task<SignOutResponse> Execute(SignOutRequest request);
    }
}
