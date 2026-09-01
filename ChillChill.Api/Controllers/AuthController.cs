using ChillChill.Contract.Auth;
using ChillChill.Contract.Users;
using Microsoft.AspNetCore.Mvc;
using ChillChill.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChillChill.Api.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly List<User> Users = [];

        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthController(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                DisplayName = request.DisplayName,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            Users.Add(user);

            var userDto = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                DisplayName = user.DisplayName
            };

            return Ok(userDto);
        }

        [HttpPost("login")]
        public IActionResult login(LoginRequest request)
        {
            var username = Users.FirstOrDefault(x => x.Username == request.Username);
            if (username is null) return Unauthorized("wrong username or password");

            var validPassword = _passwordHasher.VerifyHashedPassword(username, username.PasswordHash, request.Password);
            if (validPassword == PasswordVerificationResult.Failed) return Unauthorized("wrong username or password");

            var userLogin = new UserDTO
            {
                Id = Guid.NewGuid(),
                Username = username.Username,
                DisplayName = username.Username
            };

            return Ok(userLogin);
        }
    }
}
