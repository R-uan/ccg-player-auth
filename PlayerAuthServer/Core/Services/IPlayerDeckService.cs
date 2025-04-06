using System;
using MongoDB.Bson;
using PlayerAuthServer.Entities;

namespace PlayerAuthServer.Core.Services
{
    public interface IPlayerDeckService
    {
        /// <summary>
        /// Associates a specific deck with a player.
        /// <para>
        /// Throws <see cref="PlayerNotFoundException"/> if the player does not exist.
        /// </para>
        /// <para>
        /// DeckId must be a valid MongoDB ObjectId.
        /// </para>
        /// </summary>
        /// <param name="playerId">The unique identifier of the player.</param>
        /// <param name="deckId">The ObjectId of the deck to be linked.</param>
        /// <returns>The created <see cref="PlayerDeck"/> entity.</returns>
        Task<PlayerDeck> LinkPlayerDeckAsync(Guid playerId, ObjectId deckId);
    }
}