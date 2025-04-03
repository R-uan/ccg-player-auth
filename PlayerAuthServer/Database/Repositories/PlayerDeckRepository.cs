using System;
using Microsoft.EntityFrameworkCore;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Database.Entities;

namespace PlayerAuthServer.Database.Repositories;

public class PlayerDeckRepository(PlayerDbContext dbContext) : IPlayerDecksRepository
{
    public async Task<PlayerDeck> LinkDeck(PlayerDeck entity)
    {
        var add = await dbContext.PlayerDecks.AddAsync(entity);
        var save = await dbContext.SaveChangesAsync();
        return save > 0 ? add.Entity : throw new DbUpdateException("Could not save entity");
    }

    public async Task<bool> UnlinkDeck(PlayerDeck entity)
    {
        dbContext.PlayerDecks.Remove(entity);
        var save = await dbContext.SaveChangesAsync();
        return save > 0;
    }
}
