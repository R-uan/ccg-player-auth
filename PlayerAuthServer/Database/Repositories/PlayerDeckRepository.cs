using PlayerAuthServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace PlayerAuthServer.Database.Repositories
{
    public class PlayerDeckRepository(PlayerDbContext dbContext) : IPlayerDeckRepository
    {
        public async Task<PlayerDeck> Save(PlayerDeck entity)
        {
            var entry = await dbContext.PlayerDecks.AddAsync(entity);
            var affectedRows = await dbContext.SaveChangesAsync();

            if (affectedRows <= 0)
                throw new DbUpdateException("Could not save PlayerDeck entity.");

            return entry.Entity;
        }

        public async Task<bool> Delete(PlayerDeck entity)
        {
            dbContext.PlayerDecks.Remove(entity);
            var affectedRows = await dbContext.SaveChangesAsync();

            if (affectedRows <= 0)
                throw new DbUpdateException("Could not delete PlayerDeck entity");

            return true;
        }
    }
}