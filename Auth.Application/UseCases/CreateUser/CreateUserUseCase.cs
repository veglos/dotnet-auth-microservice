using Auth.Application.Enums;
using Auth.Application.Ports.Repositories;
using Auth.Application.Ports.Services;
using Auth.Application.UseCases.CreateUser.Request;
using Auth.Application.UseCases.CreateUser.Response;
using Auth.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Application.UseCases.CreateUser
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly ILogger _logger;
        private readonly IAuthTokenService _authTokenService;
        private readonly IAuthRepository _authRepository;
        private readonly ICryptographyService _cryptographyService;

        public CreateUserUseCase(
            ILogger<CreateUserUseCase> logger,
            IAuthTokenService authTokenService,
            IAuthRepository authRepository,
            ICryptographyService cryptographyService)
        {
            _logger = logger;
            _authTokenService = authTokenService;
            _authRepository = authRepository;
            _cryptographyService = cryptographyService;
        }

        public async Task<CreateUserResponse> Execute(CreateUserRequest request)
        {
            try
            {
                var salt = _cryptographyService.GenerateSalt();
                var currentDate = DateTime.UtcNow;

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    Email = request.Email,
                    Password = _cryptographyService.HashPassword(request.Password, salt),
                    Salt = salt,
                    Name = request.Name,
                    LastName = request.LastName,
                    Claims = ToClaims(request.Claims),
                    CreationDate = currentDate,
                    UpdateDate = currentDate
                };

                await _authRepository.CreateUser(user);

                return new CreateUserSuccessResponse
                {
                    UserId = user.Id
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var response = new CreateUserErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };

                return response;
            }
        }

        private static IList<Domain.Claim> ToClaims(IList<Request.Claim> requestClaims)
        {
            if (requestClaims == null) return null;
            var claims = new List<Domain.Claim>();
            claims.AddRange(requestClaims.Select(r => new Domain.Claim { Type = r.Type, Value = r.Value }).ToList());
            return claims;
        }
    }
}
