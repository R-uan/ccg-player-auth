using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using PlayerAuthServer.Database;
using PlayerAuthServer.Database.Repositories;
using PlayerAuthServer.Entities;

namespace PlayerAuthServer.Tests.UnitTests.Repositories;

public class PlayerDeckRepositoryTests
{
    private readonly PlayerDbContext playerDbContext;
    private readonly IPlayerDeckRepository playerDeckRepository;

    public PlayerDeckRepositoryTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<PlayerDbContext>()
            .UseInMemoryDatabase(databaseName: "playerTestDatabase").Options;

        this.playerDbContext = new PlayerDbContext(dbContextOptions);
        this.playerDeckRepository = new PlayerDeckRepository(this.playerDbContext);

        this.playerDbContext.PlayerDecks.AddRange(
            [
                new PlayerDeck(ObjectId.GenerateNewId(), Guid.NewGuid()),
                new PlayerDeck(ObjectId.GenerateNewId(), Guid.NewGuid()),
            ]
        );

        this.playerDbContext.SaveChanges();
    }

    [Fact]
    public async Task LinkDeck_ShouldReturnPlayerDeckEntity()
    {
        var playerDeck = new PlayerDeck(ObjectId.GenerateNewId(), Guid.NewGuid());
        var result = await this.playerDeckRepository.Save(playerDeck);

        Assert.NotNull(result);
        Assert.IsType<PlayerDeck>(result);
    }

    [Fact]
    public async Task UnlinkDeck_ShouldReturnTrue()
    {
        var playerDeck = await this.playerDbContext.PlayerDecks.FirstOrDefaultAsync();
        Assert.NotNull(playerDeck);

        var result = await this.playerDeckRepository.Delete(playerDeck);
        Assert.True(result);
    }
}
