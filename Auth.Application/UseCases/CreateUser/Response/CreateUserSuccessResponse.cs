using System;

namespace Auth.Application.UseCases.CreateUser.Response
{
    public class CreateUserSuccessResponse : CreateUserResponse
    {
        public Guid UserId { get; internal set; }
    }
}
