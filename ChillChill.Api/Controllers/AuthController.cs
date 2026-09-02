using ChillChill.Api.Services.Auth;
using ChillChill.Contract.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ChillChill.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.Register(request);

            if (result.IsSuccess == false)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequest request)
        {
            var result = await _authService.Login(request);

            if (result is null)
            {
                return BadRequest("Wrong Username or Password");
            }

            return Ok(result);
        }
    }
}
