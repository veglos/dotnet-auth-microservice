using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace External.API.Authorization
{
    public class GetWeatherHandler : AuthorizationHandler<GetWeatherRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetWeatherRequirement requirement)
        {
            
            if (context.User.HasClaim(c => c.Type == "scope" && c.Value.Contains("can_read_weather")))
            {
                context.Succeed(requirement);
            }

            // Scopes are not mandatory and if they are not enough, then we could go to the database of this microservice and fetch permissions with the user's ID 

            return Task.CompletedTask;
        }
    }
}
