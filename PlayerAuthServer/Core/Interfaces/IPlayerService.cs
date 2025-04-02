using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Utilities.DataTransferObjects;

namespace PlayerAuthServer.Core.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayerDto> CreatePlayer(PlayerDto player);

        Task<Player?> FindPlayer(Guid uuid);
        Task<Player?> FindPlayerByEmail(string email);
        Task<Player?> FindPlayerByNickname(string nickname);
    }
}