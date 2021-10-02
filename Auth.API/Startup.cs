using Auth.Application;
using Auth.Application.Ports.Repositories;
using Auth.Application.Ports.Services;
using Auth.Application.UseCases.Login;
using Auth.Application.UseCases.RefreshToken;
using Auth.Application.UseCases.SignOut;
using Auth.Infrastructure.Repositories.MongoDB;
using Auth.Infrastructure.Services.Cryptography;
using Auth.Infrastructure.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace Auth.API
{
    public class Startup
    {
        private IWebHostEnvironment CurrentEnvironment { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Set up AppSettings and jwtSettings
            var jwtSettingsConfiguration = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(jwtSettingsConfiguration);
            var jwtSettings = jwtSettingsConfiguration.Get<AppSettings>().AuthTokenSettings;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                if (CurrentEnvironment.IsDevelopment()) options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.FromMinutes(0)
						
                };
            })
            ;

            services.AddAuthorization(options =>
            {
                //TODO: Can we get this from the database?
                options.AddPolicy("CanReadProtectedResource", policy => policy.RequireClaim("permission", "CanReadProtectedResource"));
            });

            // Register services
            services.AddSingleton<IAuthTokenService, JwtService>();
            services.AddSingleton<ICryptographyService, CryptographyService>();

            // Register repositories
            services.AddSingleton<IAuthRepository, AuthRepository>();

            // Register use cases
            services.AddSingleton<ILoginUseCase, LoginUseCase>();
            services.AddSingleton<IRefreshTokenUseCase, RefreshTokenUseCase>();
            services.AddSingleton<ISignOutUseCase, SignOutUseCase>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CurrentEnvironment = env;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth.API v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
