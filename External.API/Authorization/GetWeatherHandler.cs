using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace External.API.Authorization
{
    public class GetWeatherHandler : AuthorizationHandler<GetWeatherRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetWeatherRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "CanReadWeather" && c.Value == "true"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
