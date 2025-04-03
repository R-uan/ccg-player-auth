using System;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Utilities.Exceptions;

namespace PlayerAuthServer.Core.Services
{
    public class PlayerDeckService(IPlayerService playerService, IPlayerDecksRepository playerDeckRepository) : IPlayerDeckService
    {
        public async Task<PlayerDeck> LinkPlayerDeck(Guid playerUUID, Guid deckUUID)
        {
            var player = await playerService.FindPlayer(playerUUID) ??
                throw new PlayerNotFoundException("Player UUID not found in the database");

            return await playerDeckRepository.LinkDeck(new()
            {
                DeckGuid = playerUUID,
                PlayerGuid = player.UUID
            });
        }
    }
}
