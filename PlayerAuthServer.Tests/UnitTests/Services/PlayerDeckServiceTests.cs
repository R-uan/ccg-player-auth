using Moq;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Core.Services;
using PlayerAuthServer.Database.Entities;

namespace PlayerAuthServer.Tests.UnitTests.Services
{
    public class PlayerDeckServiceTests
    {
        private readonly IPlayerDeckService playerDeckService;
        private readonly Mock<IPlayerService> mockPlayerService;
        private readonly Mock<IPlayerDeckRepository> mockPlayerDeckRepository;

        public PlayerDeckServiceTests()
        {
            this.mockPlayerService = new Mock<IPlayerService>();
            this.mockPlayerDeckRepository = new Mock<IPlayerDeckRepository>();
            this.playerDeckService = new PlayerDeckService(this.mockPlayerService.Object, this.mockPlayerDeckRepository.Object);
        }

        [Fact]
        public async Task LinkPlayerDeck_ShouldReturnPlayerDeckEntity()
        {
            var mockPlayer = new Player
            {
                Email = "",
                Nickname = "",
                PasswordHash = "",
                UUID = Guid.NewGuid()
            };

            var mockPlayerDeck = new PlayerDeck
            {
                PlayerGuid = mockPlayer.UUID,
                DeckGuid = Guid.NewGuid(),
            };

            this.mockPlayerDeckRepository.Setup(repository => repository.LinkDeck(It.IsAny<PlayerDeck>()))
                .ReturnsAsync(mockPlayerDeck);
            this.mockPlayerService.Setup(service => service.FindPlayer(It.IsAny<Guid>()))
                .ReturnsAsync(mockPlayer);

            var result = await this.playerDeckService.LinkPlayerDeck(mockPlayer.UUID, mockPlayerDeck.DeckGuid);

            Assert.NotNull(result);
            Assert.IsType<PlayerDeck>(result);
        }
    }
}