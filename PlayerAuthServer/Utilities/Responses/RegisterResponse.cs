using PlayerAuthServer.Entities.Models;
using PlayerAuthServer.Models;

namespace PlayerAuthServer.Utilities.Responses
{
    public class RegisterResponse(PartialPlayerProfile player)
    {
        public PartialPlayerProfile Player { get; set; } = player;
    }
}