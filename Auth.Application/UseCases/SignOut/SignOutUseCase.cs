using Auth.Application.Enums;
using Auth.Application.Ports.Repositories;
using Auth.Application.UseCases.SignOut.Request;
using Auth.Application.UseCases.SignOut.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Auth.Application.UseCases.SignOut
{
    public class SignOutUseCase : ISignOutUseCase
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _authRepository;

        public SignOutUseCase(
            ILogger<SignOutUseCase> logger,
            IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }

        public async Task<SignOutResponse> Execute(SignOutRequest request)
        {
            try
            {
                var user = await _authRepository.GetUserByUserId(request.UserId);
                if (user == null)
                {
                    return new SignOutErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.UserDoesNotExist),
                        Code = ErrorCodes.UserDoesNotExist.ToString("D")
                    };
                }
                user.RefreshToken.Active = false;
                await _authRepository.UpdateUser(user);

                return new SignOutSuccessResponse
                {
                    Message = $"User signed out at {DateTime.UtcNow} server time."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new SignOutErrorResponse
                {
                    Code = "Some Error Code",
                    Message = "Some Error Message"
                };
            }
        }
    }
}
