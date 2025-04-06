using PlayerAuthServer.Entities.Models;

namespace PlayerAuthServer.Utilities.Responses
{
    public class RegisterResponse(PlayerDto player)
    {
        public PlayerDto Player { get; set; } = player;
    }
}