namespace Auth.Application.UseCases.Login.Response
{
    public class LoginErrorResponse : LoginResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}