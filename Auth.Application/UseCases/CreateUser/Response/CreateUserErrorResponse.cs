namespace Auth.Application.UseCases.CreateUser.Response
{
    public class CreateUserErrorResponse : CreateUserResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}
