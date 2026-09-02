using ChillChill.Contract.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChillChill.Services
{
    public interface IApiClient
    {
        Task<AuthResult> RegisterAsync(RegisterRequest registerRequest);
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}
