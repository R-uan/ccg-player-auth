using Microsoft.EntityFrameworkCore;
using PlayerAuthServer.Entities;

namespace PlayerAuthServer.Database.Repositories
{
    public class PlayerRepository(PlayerDbContext dbContext) : IPlayerRepository
    {
        public async Task<Player> Save(Player player)
        {
            var entry = await dbContext.Players.AddAsync(player);
            int affectedRows = await dbContext.SaveChangesAsync();

            if (affectedRows <= 0)
                throw new DbUpdateException("Could not insert Player entity.");

            return entry.Entity;
        }

        public async Task<Player?> FindPlayer(Guid Id)
            => await (from p in dbContext.Players
                      where p.Id == Id
                      select p).FirstOrDefaultAsync();

        public async Task<Player?> FindPlayerByEmail(string email)
                    => await (from p in dbContext.Players
                              where p.Email.ToLower() == email.ToLower()
                              select p).FirstOrDefaultAsync();


        public async Task<Player?> FindPlayerByUsername(string Username)
                    => await (from p in dbContext.Players
                              where p.Username.ToLower() == Username.ToLower()
                              select p).FirstOrDefaultAsync();
    }
}