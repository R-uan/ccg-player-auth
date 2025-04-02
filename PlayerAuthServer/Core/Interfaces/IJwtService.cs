using PlayerAuthServer.Database.Entities;

namespace PlayerAuthServer.Core.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Player player);
    }
}