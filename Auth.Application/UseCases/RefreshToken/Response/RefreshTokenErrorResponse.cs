namespace Auth.Application.UseCases.RefreshToken.Response
{
    public class RefreshTokenErrorResponse : RefreshTokenResponse
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
