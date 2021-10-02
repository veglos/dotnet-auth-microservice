using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("GetPublicResource")]
        public bool GetPublicResource()
        {
            return true;
        }

        [HttpGet]
        [Route("GetPrivateResourceWithoutPolicy")]
        public bool GetPrivateResourceWithoutPolicy()
        {
            return true;
        }

        [Authorize(Policy = "CanReadProtectedResource")]
        [HttpGet]
        [Route("GetPrivateResourceWithPolicy")]
        public bool GetPrivateResourceWithPolicy()
        {
            return true;
        }
    }
}
