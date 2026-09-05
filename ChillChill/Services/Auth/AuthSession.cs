using ChillChill.Contract.Users;

namespace ChillChill.Services.Auth
{
    public class AuthSession : IAuthSession
    {
        public string? Token { get; set; }
        public UserDTO User { get; set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(Token) && User != null;

        public void ClearSession()
        {
            Token = null;
            User = null!;
        }
    }
}
