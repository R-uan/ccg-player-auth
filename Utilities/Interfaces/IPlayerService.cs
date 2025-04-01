using System;
using PlayerAuthServer.Database.DataTransferObject;
using PlayerAuthServer.Database.Entities;

namespace PlayerAuthServer.Interfaces;

public interface IPlayerService
{
    Task<PlayerDto> CreatePlayer(PlayerDto player);

    Task<Player?> FindPlayer(Guid uuid);
    Task<Player?> FindPlayerByEmail(string email);
    Task<Player?> FindPlayerByNickname(string nickname);
}
