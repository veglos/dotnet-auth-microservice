using Auth.Application.Enums;
using Auth.Application.Exceptions;
using Auth.Application.Ports.Repositories;
using Auth.Application.Ports.Services;
using Auth.Application.UseCases.RefreshToken.Request;
using Auth.Application.UseCases.RefreshToken.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Auth.Application.UseCases.RefreshToken
{
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        private readonly ILogger _logger;
        private readonly IAuthTokenService _authTokenService;
        private readonly IAuthRepository _authRepository;

        public RefreshTokenUseCase(
            ILogger<RefreshTokenUseCase> logger,
            IAuthTokenService authTokenService,
            IAuthRepository authRepository)
        {
            _logger = logger;
            _authTokenService = authTokenService;
            _authRepository = authRepository;
        }

        public async Task<RefreshTokenResponse> Execute(RefreshTokenRequest request)
        {
            try
            {
                if (!await _authTokenService.IsTokenValid(request.AccessToken, false))
                {
                    return new RefreshTokenErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.AccessTokenIsNotValid),
                        Code = ErrorCodes.AccessTokenIsNotValid.ToString("D")
                    };
                }

                var userId = await _authTokenService.GetUserIdFromToken(request.AccessToken);
                var user = await _authRepository.GetUserByUserId(userId);

                if (!user.RefreshToken.Active)
                {
                    return new RefreshTokenErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.RefreshTokenIsNotActive),
                        Code = ErrorCodes.RefreshTokenIsNotActive.ToString("D")
                    };
                }

                if (user.RefreshToken.ExpirationDate < DateTime.UtcNow)
                {
                    return new RefreshTokenErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.RefreshTokenHasExpired),
                        Code = ErrorCodes.RefreshTokenHasExpired.ToString("D")
                    };
                }

                if (user.RefreshToken.Value != request.RefreshToken)
                {
                    return new RefreshTokenErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.RefreshTokenIsNotCorrect),
                        Code = ErrorCodes.RefreshTokenIsNotCorrect.ToString("D")
                    };
                }

                var newToken = await _authTokenService.GenerateAccessToken(user);

                user.RefreshToken.Value = await _authTokenService.GenerateRefreshToken();
                user.RefreshToken.Active = true;
                user.RefreshToken.ExpirationDate = DateTime.UtcNow.AddMinutes(await _authTokenService.GetRefreshTokenLifetimeInMinutes());
                await _authRepository.UpdateUser(user);

                var response = new RefreshTokenSuccessResponse
                {
                    AccessToken = newToken,
                    RefreshToken = user.RefreshToken.Value,
                    RefreshTokenExpirationDate = user.RefreshToken.ExpirationDate
                };

                return response;
            }
            catch (InvalidTokenException ex)
            {
                _logger.LogError(ex, ex.Message);

                var response = new RefreshTokenErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AccessTokenIsNotValid),
                    Code = ErrorCodes.AccessTokenIsNotValid.ToString("D")
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var response = new RefreshTokenErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };

                return response;
            }
        }
    }
}
