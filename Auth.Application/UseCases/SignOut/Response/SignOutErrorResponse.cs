namespace Auth.Application.UseCases.SignOut.Response
{
    public class SignOutErrorResponse : SignOutResponse
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
