using System.Collections.Generic;

namespace Auth.Application.UseCases.CreateUser.Request
{
    public class CreateUserRequest : UseCases.Request
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public IList<Claim> Claims { get; set; }
    }
}
