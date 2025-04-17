using PlayerAuthServer.Models;

namespace PlayerAuthServer.Utilities.Responses
{
    public record RegisterResponse(PartialPlayerProfile Player);
}