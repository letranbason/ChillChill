using ChillChill.Contract.Auth;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChillChill.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResult> RegisterAsync(RegisterRequest registerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", registerRequest);
            var result = await response.Content.ReadFromJsonAsync<AuthResult>();
            return result;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginRequest);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null!;
            }

            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }
    }
}
