using System.ComponentModel.DataAnnotations;

namespace ChillChill.Contract.Auth
{
    public sealed class RegisterRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string DisplayName { get; set; } = string.Empty;
    }
}
