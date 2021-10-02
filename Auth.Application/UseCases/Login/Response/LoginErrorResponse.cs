namespace Auth.Application.UseCases.Login.Response
{
    public class LoginErrorResponse : LoginResponse
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
}