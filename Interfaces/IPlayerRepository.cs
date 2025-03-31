using PlayerAuthServer.Database.Entities;

namespace PlayerAuthServer.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player?> CreatePlayer(Player player);

        Task<Player?> FindPlayer(Guid uuid);
        Task<Player?> FindPlayerByEmail(string email);
        Task<Player?> FindPlayerByNickname(string nickname);
    }
}