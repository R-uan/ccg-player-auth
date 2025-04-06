using PlayerAuthServer.Entities;
using PlayerAuthServer.Entities.Models;
using PlayerAuthServer.Database.Repositories;

namespace PlayerAuthServer.Core.Services
{
    public class PlayerService(IPlayerRepository playerRepository) : IPlayerService
    {
        public async Task<Player> CreatePlayerWithDefaults(NewPlayer newPlayer)
        {
            var player = new Player
            {
                Email = newPlayer.Email,
                Username = newPlayer.Username,
                PasswordHash = newPlayer.PasswordHash,
            };

            // Need to add default decks (I'll do it later chill)

            var createdPlayer = await playerRepository.Save(player);
            return createdPlayer;
        }
    }
}
