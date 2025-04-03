using System;
using Microsoft.EntityFrameworkCore;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Database;
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Database.Repositories;

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
                new PlayerDeck {DeckGuid = Guid.NewGuid(), PlayerGuid = Guid.NewGuid()},
                new PlayerDeck {DeckGuid = Guid.NewGuid(), PlayerGuid = Guid.NewGuid()},
            ]
        );

        this.playerDbContext.SaveChanges();
    }

    [Fact]
    public async Task LinkDeck_ShouldReturnPlayerDeckEntity()
    {
        var playerDeck = new PlayerDeck { DeckGuid = Guid.NewGuid(), PlayerGuid = Guid.NewGuid() };
        var result = await this.playerDeckRepository.LinkDeck(playerDeck);
        Assert.NotNull(result);
        Assert.IsType<PlayerDeck>(result);
    }

    [Fact]
    public async Task UnlinkDeck_ShouldReturnTrue()
    {
        var playerDeck = await this.playerDbContext.PlayerDecks.FirstOrDefaultAsync();
        Assert.NotNull(playerDeck);
        var result = await this.playerDeckRepository.UnlinkDeck(playerDeck);
        Assert.True(result);
    }
}
