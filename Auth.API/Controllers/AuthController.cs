using System.Threading.Tasks;
using Auth.Application.UseCases.CreateUser;
using Auth.Application.UseCases.CreateUser.Request;
using Auth.Application.UseCases.CreateUser.Response;
using Auth.Application.UseCases.Login;
using Auth.Application.UseCases.Login.Request;
using Auth.Application.UseCases.Login.Response;
using Auth.Application.UseCases.RefreshToken;
using Auth.Application.UseCases.RefreshToken.Request;
using Auth.Application.UseCases.RefreshToken.Response;
using Auth.Application.UseCases.SignOut;
using Auth.Application.UseCases.SignOut.Request;
using Auth.Application.UseCases.SignOut.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly RefreshTokenUseCase _refreshTokenUseCase;
        private readonly SignOutUseCase _signOutUseCase;
        private readonly CreateUserUseCase _createUserUseCase;

        public AuthController(
            LoginUseCase loginUseCase,
            RefreshTokenUseCase refreshTokenUseCase,
            SignOutUseCase signOutUseCase,
            CreateUserUseCase createUserUseCase)
        {
            _loginUseCase = loginUseCase;
            _refreshTokenUseCase = refreshTokenUseCase;
            _signOutUseCase = signOutUseCase;
            _createUserUseCase = createUserUseCase;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await _loginUseCase.Execute(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
        {
            return await _refreshTokenUseCase.Execute(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SignOut")]
        public async Task<SignOutResponse> SignOut(SignOutRequest request)
        {
            return await _signOutUseCase.Execute(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CreateUser")]
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            return await _createUserUseCase.Execute(request);
        }
    }
}
