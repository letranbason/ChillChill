using ChillChill.Contract.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChillChill.Contract.Auth
{
    public class LoginReponse
    {
        public string Token { get; set; } = string.Empty;
        public UserDTO User { get; set; } = null!;
    }
}
