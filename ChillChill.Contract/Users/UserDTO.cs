using System;
using System.Collections.Generic;
using System.Text;

namespace ChillChill.Contract.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
