using ChillChill.Api.Data;
using ChillChill.Api.Entities;
using ChillChill.Contract.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChillChill.Contract.Auth;
using ChillChill.Api.Services.Token;

namespace ChillChill.Api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(AppDbContext context, IPasswordHasher<User> passwordHasher, ITokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> Register(RegisterRequest request)
        {
            var userExist = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == request.Username.ToLower());
            if (userExist is not null)
            {
                return new AuthResult { IsSuccess = false, ErrorMessage = "Username already exists." };
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                DisplayName = request.DisplayName,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                DisplayName = user.DisplayName
            };

            return new AuthResult { IsSuccess = true, User = userDto };
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var username = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
            if (username is null) return null!;

            var validPassword = _passwordHasher.VerifyHashedPassword(username, username.PasswordHash, request.Password);
            if (validPassword == PasswordVerificationResult.Failed) return null!;

            var userLogin = new UserDTO
            {
                Id = Guid.NewGuid(),
                Username = username.Username,
                DisplayName = username.Username
            };

            var token = _tokenService.CreateToken(username);

            return new LoginResponse { Token = token , User = userLogin};
        }
    }
}
