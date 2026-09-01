using ChillChill.Api.Entities;

namespace ChillChill.Api.Services.Token
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
