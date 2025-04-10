using Microsoft.EntityFrameworkCore;
using PlayerAuthServer.Database;
using PlayerAuthServer.Interfaces;
using PlayerAuthServer.Models;

namespace PlayerAuthServer.Repositories
{
    public class CardCollectionRepository(PlayerDbContext dbContext) : ICardCollectionRepository
    {
        public async Task<CardCollection> SaveEntity(CardCollection entity)
        {
            var save = await dbContext.CardCollection.AddAsync(entity);
            var affectedRows = await dbContext.SaveChangesAsync();
            if (affectedRows <= 0) throw new DbUpdateException("Could not insert CardCollection entity.");
            return save.Entity;
        }

        public async Task<List<CardCollection>> FindPlayerCardCollection(Guid playerId)
            => await dbContext.CardCollection.Where(oc => oc.PlayerId == playerId).ToListAsync();
    }
}
