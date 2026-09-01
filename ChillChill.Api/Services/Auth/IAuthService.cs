using ChillChill.Contract.Auth;
using ChillChill.Contract.Users;

namespace ChillChill.Api.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResult> Register(RegisterRequest request);
        Task<LoginReponse> Login(LoginRequest request);
    }
}
