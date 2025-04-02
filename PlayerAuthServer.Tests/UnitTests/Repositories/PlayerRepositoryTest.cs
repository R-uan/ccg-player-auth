using Microsoft.EntityFrameworkCore;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Database;
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Database.Repositories;

namespace PlayerAuthServer.Tests.UnitTests.Repositories
{
    public class PlayerRepositoryTest
    {
        private readonly PlayerDbContext playerDbContext;
        private readonly IPlayerRepository playerRepository;

        public PlayerRepositoryTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<PlayerDbContext>()
                .UseInMemoryDatabase(databaseName: "playerTestDatabase").Options;
            this.playerDbContext = new PlayerDbContext(dbContextOptions);
            this.playerRepository = new PlayerRepository(this.playerDbContext);

            var players = new List<Player>
            {
                new() { Email = "player1@example.com", Nickname = "PlayerOne", PasswordHash = "hash1", Decks = new List<PlayerDeck>() },
                new() { Email = "player2@example.com", Nickname = "PlayerTwo", PasswordHash = "hash2", Decks = new List<PlayerDeck>() },
                new() { Email = "player3@example.com", Nickname = "PlayerThree", PasswordHash = "hash3", Decks = new List<PlayerDeck>() },
                new() { Email = "player4@example.com", Nickname = "PlayerFour", PasswordHash = "hash4", Decks = new List<PlayerDeck>() },
                new() { Email = "player5@example.com", Nickname = "PlayerFive", PasswordHash = "hash5", Decks = new List<PlayerDeck>() }
            };

            this.playerDbContext.Players.AddRange(players);
            this.playerDbContext.SaveChanges();
        }

        [Fact]
        public async Task ShouldFindPlayerByNickname()
        {
            var player = await this.playerRepository.FindPlayerByNickname("playerone");
            Assert.NotNull(player);
        }

        [Fact]
        public async Task ShouldNotFindPlayerByNickname()
        {
            var player = await this.playerRepository.FindPlayerByNickname("notexist");
            Assert.Null(player);
        }

        [Fact]
        public async Task ShouldFindPlayerByEmail()
        {
            var player = await this.playerRepository.FindPlayerByEmail("player4@example.com");
            Assert.NotNull(player);
        }

        [Fact]
        public async Task ShouldNotFindPlayerByEmail()
        {
            var player = await this.playerRepository.FindPlayerByEmail("notexist@coldmail.com");
            Assert.Null(player);
        }

        [Fact]
        public async Task ShouldFindPlayerByGuid()
        {
            Player newPlayer = new() { Email = "wen@wen.wen", Nickname = "wen", PasswordHash = "wen" };
            var playerEntity = await this.playerDbContext.Players.AddAsync(newPlayer);
            await this.playerDbContext.SaveChangesAsync();
            var playerUuid = playerEntity.Entity.UUID;
            var player = await this.playerRepository.FindPlayer(playerUuid);
            Assert.NotNull(player);
        }

        [Fact]
        public async Task ShouldNotFindPlayerByGuid()
        {
            var player = await this.playerRepository.FindPlayer(Guid.NewGuid());
            Assert.Null(player);
        }

        [Fact]
        public async Task ShouldCreatePlayerEntity()
        {
            Player newPlayer = new() { Email = "new@new.new", Nickname = "new", PasswordHash = "wen" };
            var player = await this.playerRepository.CreatePlayer(newPlayer);
            Assert.NotNull(player);
            Assert.IsType<Player>(player);
        }
    }
}
