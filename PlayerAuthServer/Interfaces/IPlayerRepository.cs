using PlayerAuthServer.Entities;

namespace PlayerAuthServer.Database.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> SavePlayer(Player player);
        Task<Player?> FindPlayer(Guid Id);
        Task<Player?> FindPlayerByEmail(string email);
        Task<Player?> FindPlayerByUsername(string Username);
    }
}