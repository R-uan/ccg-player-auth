using PlayerAuthServer.Entities;

namespace PlayerAuthServer.Database.Repositories
{
    public interface IPlayerDeckRepository
    {
        /// <summary>
        /// Persists a new PlayerDeck entity to the database.
        /// Throws <see cref="DbUpdateException"/> if the save operation fails.
        /// </summary>
        /// <param name="entity">The PlayerDeck to save.</param>
        /// <returns>The saved <see cref="PlayerDeck"/> entity.</returns>
        Task<PlayerDeck> Save(PlayerDeck entity);

        /// <summary>
        /// Removes the association between a player and a deck.
        /// Throws <see cref="DbUpdateException"/> if the delete operation fails.
        /// </summary>
        /// <param name="entity">The <see cref="PlayerDeck"/> entity to remove.</param>
        /// <returns>True if the entity was successfully deleted.</returns>
        Task<bool> Delete(PlayerDeck entity);
    }
}