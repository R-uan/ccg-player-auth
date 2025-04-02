using Microsoft.EntityFrameworkCore;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Database.Entities;

namespace PlayerAuthServer.Database.Repositories
{
    public class PlayerRepository(PlayerDbContext dbContext) : IPlayerRepository
    {
        public async Task<Player?> CreatePlayer(Player player)
        {
            var save = await dbContext.Players.AddAsync(player);
            int changes = await dbContext.SaveChangesAsync();
            return changes > 0 ? save.Entity : null;
        }

        public async Task<Player?> FindPlayer(Guid uuid)
            => await (from p in dbContext.Players
                      where p.UUID == uuid
                      select p).FirstOrDefaultAsync();

        public async Task<Player?> FindPlayerByEmail(string email)
                    => await (from p in dbContext.Players
                              where p.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)
                              select p).FirstOrDefaultAsync();


        public async Task<Player?> FindPlayerByNickname(string nickname)
                    => await (from p in dbContext.Players
                              where p.Nickname.Equals(nickname, StringComparison.CurrentCultureIgnoreCase)
                              select p).FirstOrDefaultAsync();
    }
}