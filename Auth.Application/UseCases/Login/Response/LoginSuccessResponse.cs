namespace Auth.Application.UseCases.Login.Response
{
    public class LoginSuccessResponse : LoginResponse
    {
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
