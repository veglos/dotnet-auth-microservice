using Auth.Application.Enums;
using Auth.Application.Ports.Repositories;
using Auth.Application.Ports.Services;
using Auth.Application.UseCases.Login.Request;
using Auth.Application.UseCases.Login.Response;
using Auth.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Auth.Application.UseCases.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly ILogger _logger;
        private readonly IAuthTokenService _authTokenService;
        private readonly IAuthRepository _authRepository;
        private readonly ICryptographyService _cryptographyService;

        public LoginUseCase(
            ILogger<LoginUseCase> logger,
            IAuthTokenService authTokenService,
            IAuthRepository authRepository,
            ICryptographyService cryptographyService)
        {
            _logger = logger;
            _authTokenService = authTokenService;
            _authRepository = authRepository;
            _cryptographyService = cryptographyService;
        }

        public async Task<LoginResponse> Execute(LoginRequest request)
        {
            try
            {
                var user = await _authRepository.GetUserByEmail(request.Email);
                if (user == null)
                {
                    var response = new LoginErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.UserDoesNotExist),
                        Code = ErrorCodes.UserDoesNotExist.ToString("D")
                    };
                    return response;
                }

                if (AreCredentialsValid(request.Password, user))
                {
                    user.RefreshToken = new Domain.RefreshToken
                    {
                        Value = await _authTokenService.GenerateRefreshToken(),
                        Active = true,
                        ExpirationDate = DateTime.UtcNow.AddMinutes(await _authTokenService.GetRefreshTokenLifetimeInMinutes())
                    };
                    await _authRepository.UpdateUser(user);

                    var idToken = await _authTokenService.GenerateIdToken(user);
                    var accessToken = await _authTokenService.GenerateAccessToken(user);

                    var response = new LoginSuccessResponse
                    {
                        IdToken = idToken,
                        AccessToken = accessToken,
                        RefreshToken = user.RefreshToken.Value
                    };

                    return response;
                }
                else
                {
                    var response = new LoginErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.CredentialsAreNotValid),
                        Code = ErrorCodes.CredentialsAreNotValid.ToString("D")
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var response = new LoginErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };

                return response;
            }
        }

        private bool AreCredentialsValid(string testPassword, User user)
        {
            var hash = _cryptographyService.HashPassword(testPassword, user.Salt);
            return hash == user.Password;
        }
    }
}
