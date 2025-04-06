using MongoDB.Bson;
using PlayerAuthServer.Database.Repositories;
using PlayerAuthServer.Entities;
using PlayerAuthServer.Utilities.Exceptions;

namespace PlayerAuthServer.Core.Services
{
    public class PlayerDeckService(IPlayerRepository playerRepository, IPlayerDeckRepository playerDeckRepository) : IPlayerDeckService
    {
        public async Task<PlayerDeck> LinkPlayerDeckAsync(Guid playerId, ObjectId deckId)
        {
            var _ = await playerRepository.FindPlayer(playerId)
                ?? throw new PlayerNotFoundException("Unable to find player with the given ID.");
            var playerDeck = new PlayerDeck(deckId, playerId);
            return await playerDeckRepository.Save(playerDeck);
        }
    }
}
