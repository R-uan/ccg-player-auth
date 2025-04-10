using System.ComponentModel.DataAnnotations;

namespace PlayerAuthServer.Entities.Models
{
    public class PlayerDto
    {
        public Guid Id { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Username required")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 16 characters")]
        public required string Username { get; set; }
    }
}