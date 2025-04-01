using System.ComponentModel.DataAnnotations;

namespace PlayerAuthServer.Database.DataTransferObject
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public required string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public required string Password { get; set; }
    }
}