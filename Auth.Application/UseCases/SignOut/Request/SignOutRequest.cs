using System;

namespace Auth.Application.UseCases.SignOut.Request
{
    public class SignOutRequest : UseCases.Request
    {
        public Guid UserId { get; set; }
    }
}
