namespace Auth.Application
{
    public class AppSettings
    {
        public AuthTokenSettings AuthTokenSettings { get; set; }
        public RefreshTokenSettings RefreshTokenSettings { get; set; }
        public RepositorySettings RepositorySettings { get; set; }
    }

    public class RefreshTokenSettings
    {
        public int Length { get; set; }
        public int LifeTimeInMinutes { get; set; }
    }

    public class AuthTokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public long LifeTimeInSeconds { get; set; }
        public string SecretKey { get; set; }
    }

    public class RepositorySettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
