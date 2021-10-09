using Auth.Application.UseCases.CreateUser.Request;
using Auth.Application.UseCases.CreateUser.Response;

namespace Auth.Application.UseCases.CreateUser
{
    public interface ICreateUserUseCase : IUseCase<CreateUserRequest, CreateUserResponse>
    {
    }
}
