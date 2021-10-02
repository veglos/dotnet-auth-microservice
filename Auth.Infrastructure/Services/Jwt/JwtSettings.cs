﻿namespace Auth.Infrastructure.Services.Jwt
{
    public class JwtSettings
    {
        public AuthTokenSettings AuthTokenSettings { get; set; }
        public RefreshTokenSettings RefreshTokenSettings { get; set; }
    }

    public class AuthTokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public long LifeTimeInSeconds { get; set; }
        public string SecretKey { get; set; }
    }

    public class RefreshTokenSettings
    {
        public int Length { get; set; }
        public int LifeTimeInMinutes { get; set; }
    }
}