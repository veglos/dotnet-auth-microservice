namespace Auth.Application.UseCases.RefreshToken.Request
{
    public class RefreshTokenRequest : UseCases.Request
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
