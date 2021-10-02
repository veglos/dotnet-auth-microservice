namespace Auth.Application.UseCases.Login.Request
{
    public class LoginRequest : UseCases.Request
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
