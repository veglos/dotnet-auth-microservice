using Auth.Application.Exceptions;
using Auth.Application.Ports.Services;
using Auth.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Services.Jwt
{
    public class JwtService : IAuthTokenService
    {
        private readonly IOptions<JwtSettings> _settings;
        private readonly RsaSecurityKey _rsaSecurityKey;

        public JwtService(IOptions<JwtSettings> settings, RsaSecurityKey rsaSecurityKey)
        {
            _settings = settings;
            _rsaSecurityKey = rsaSecurityKey;
        }

        public Task<string> GenerateToken(User user)
        {
            var signingCredentials = new SigningCredentials(
                key: _rsaSecurityKey,
                algorithm: SecurityAlgorithms.RsaSha256
            );

            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claimsIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Name, user.Name));
            claimsIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Email, user.Email));
            claimsIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.GivenName, user.Name));
            claimsIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Surname, user.LastName));

            foreach (var c in user.Claims ?? System.Linq.Enumerable.Empty<Domain.Claim>())
            {
                claimsIdentity.AddClaim(new System.Security.Claims.Claim(c.Type, c.Value));
            }

            var jwtHandler = new JwtSecurityTokenHandler();

            var jwt = jwtHandler.CreateJwtSecurityToken(
                issuer: _settings.Value.AuthTokenSettings.Issuer,
                audience: _settings.Value.AuthTokenSettings.Audience,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(_settings.Value.AuthTokenSettings.LifeTimeInSeconds),
                issuedAt: DateTime.UtcNow,
                signingCredentials: signingCredentials);

            var serializedJwt = jwtHandler.WriteToken(jwt);

            return Task.FromResult(serializedJwt);
        }

        public Task<string> GenerateRefreshToken()
        {
            var size = _settings.Value.RefreshTokenSettings.Length;
            var buffer = new byte[size];
            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return Task.FromResult(Convert.ToBase64String(buffer));
        }

        public Task<int> GetRefreshTokenLifetimeInMinutes()
        {
            return Task.FromResult(_settings.Value.RefreshTokenSettings.LifeTimeInMinutes);
        }

        public Task<Guid> GetUserIdFromToken(string token)
        {
            try
            {
                using RSA rsa = RSA.Create();
                rsa.ImportRSAPrivateKey(
                    source: Convert.FromBase64String(_settings.Value.AuthTokenSettings.PublicKey),
                    bytesRead: out int _);

                var rsaKey = new RsaSecurityKey(rsa);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false, // we may be trying to validate an expired token so it makes no sense checking for it's lifetime.
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _settings.Value.AuthTokenSettings.Issuer,
                    ValidAudience = _settings.Value.AuthTokenSettings.Audience,
                    IssuerSigningKey = rsaKey,
                    ClockSkew = TimeSpan.FromMinutes(0)
                };

                var jwtHandler = new JwtSecurityTokenHandler();
                var claims = jwtHandler.ValidateToken(token, tokenValidationParameters, out _);
                var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);

                return Task.FromResult(userId);
            }
            catch (Exception ex)
            {
                throw new InvalidTokenException(ex.Message, ex);
            }
        }

        public Task<bool> IsTokenValid(string token, bool validateLifeTime)
        {
            using RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(
                source: Convert.FromBase64String(_settings.Value.AuthTokenSettings.PublicKey),
                bytesRead: out int _);

            var rsaKey = new RsaSecurityKey(rsa);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = validateLifeTime,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _settings.Value.AuthTokenSettings.Issuer,
                ValidAudience = _settings.Value.AuthTokenSettings.Audience,
                IssuerSigningKey = rsaKey,
                ClockSkew = TimeSpan.FromMinutes(0)
            };

            var jwtHandler = new JwtSecurityTokenHandler();
            try
            {
                jwtHandler.ValidateToken(token, tokenValidationParameters, out _);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }


    }
}
