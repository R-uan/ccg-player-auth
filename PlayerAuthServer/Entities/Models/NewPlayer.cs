namespace PlayerAuthServer.Entities.Models
{
    public class NewPlayer
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
    }
}