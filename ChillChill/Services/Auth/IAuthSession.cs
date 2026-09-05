using ChillChill.Contract.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChillChill.Services.Auth
{
    public interface IAuthSession
    {
        string? Token { get; set; }
        UserDTO User { get; set; }
        bool IsLoggedIn { get; set; }
        void ClearSession();
    }
}
