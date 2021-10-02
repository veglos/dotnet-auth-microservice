namespace Auth.Application.UseCases.Login.Response
{
    public class LoginSuccessResponse : LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
