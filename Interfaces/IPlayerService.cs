using System;
using PlayerAuthServer.Database.DataTransferObject;
using PlayerAuthServer.Database.Entities;

namespace PlayerAuthServer.Interfaces;

public interface IPlayerService
{
    Task<Player?> FindPlayer(Guid uuid);
    Task<bool> CreatePlayer(PlayerDto player);
}
