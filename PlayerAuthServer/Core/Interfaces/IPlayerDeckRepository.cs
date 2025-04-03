using System;
using PlayerAuthServer.Database.Entities;

namespace PlayerAuthServer.Core.Interfaces
{
    public interface IPlayerDeckRepository
    {
        /// <summary>
        ///     Links a deck identification to a player
        /// </summary>
        /// <param name="player">Authenticated owner of the deck</param>
        /// <param name="deckUUID">Identification Id of the deck located on the deck database</param>
        /// <returns>Junction Entity</returns>
        Task<PlayerDeck> LinkDeck(PlayerDeck entity);

        /// <summary>
        /// Deletes assotiation between the authenticated player and the deck
        /// </summary>
        /// <param name="deckUUID">Deck for deletion</param>
        /// <returns>True if deleted, False otherwise</returns>
        Task<bool> UnlinkDeck(PlayerDeck entity);
    }
}