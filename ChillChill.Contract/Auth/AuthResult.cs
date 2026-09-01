using ChillChill.Contract.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChillChill.Contract.Auth
{
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public UserDTO? User { get; set; }
    }
}
