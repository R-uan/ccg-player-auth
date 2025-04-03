using System;
using PlayerAuthServer.Database.Entities;

namespace PlayerAuthServer.Core.Interfaces
{
    public interface IPlayerDeckService
    {
        Task<PlayerDeck> LinkPlayerDeck(Guid playerUUID, Guid deckUUID);
    }
}