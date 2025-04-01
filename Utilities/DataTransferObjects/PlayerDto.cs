using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlayerAuthServer.Utilities.DataTransferObjects
{
    public class PlayerDto
    {
        public Guid UUID { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Nickname required")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Nickname must be between 4 and 16 characters")]
        public required string Nickname { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        public required string PasswordHash { get; set; }
    }
}