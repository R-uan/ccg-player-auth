using Moq;
using PlayerAuthServer.Database.Repositories;
using PlayerAuthServer.Entities;
using PlayerAuthServer.Interfaces;
using PlayerAuthServer.Models;
using PlayerAuthServer.Services;
using PlayerAuthServer.Utilities.Requests;

namespace PlayerAuthServer.Tests.UnitTests.Services
{
    public class CardCollectionServiceTest
    {
        private readonly ICardCollectionService cardCollectionService;
        private readonly Mock<IPlayerRepository> mockPlayerRepository;
        private readonly Mock<ICardCollectionRepository> mockCardCollectionRepository;

        public CardCollectionServiceTest()
        {
            this.mockPlayerRepository = new Mock<IPlayerRepository>();
            this.mockCardCollectionRepository = new Mock<ICardCollectionRepository>();
            this.cardCollectionService = new CardCollectionService(
                this.mockCardCollectionRepository.Object,
                this.mockPlayerRepository.Object
            );
        }

        [Fact]
        public async Task CollectCard_ShouldReturnCardCollectionEntity()
        {
            var collectCard = new CollectCardRequest
            {
                Amount = 2,
                CardId = Guid.NewGuid()
            };

            var cardCollection = new CardCollection(Guid.NewGuid(), 2, Guid.NewGuid());
            this.mockCardCollectionRepository.Setup(c => c.SaveEntity(It.IsAny<CardCollection>()))
                .ReturnsAsync(cardCollection);

            var result = await this.cardCollectionService.CollectCard(collectCard, Guid.NewGuid());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindPlayerCollection_ShouldReturnListOfCardCollection()
        {
            var player = new Player { Id = Guid.NewGuid(), Email = "", PasswordHash = "", Username = "" };
            List<CardCollection> cardCollection = [
                new CardCollection(Guid.NewGuid(), 2, player.Id),
                new CardCollection(Guid.NewGuid(), 2, player.Id)
            ];

            this.mockPlayerRepository.Setup(p => p.FindPlayer(It.IsAny<Guid>()))
                .ReturnsAsync(player);

            this.mockCardCollectionRepository.Setup(c => c.FindPlayerCardCollection(It.IsAny<Guid>()))
                .ReturnsAsync(cardCollection);

            var result = await this.cardCollectionService.FindPlayerCollection(player.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindPlayerCollection_ShouldReturnNull()
        {
            this.mockPlayerRepository.Setup(p => p.FindPlayer(It.IsAny<Guid>()))
                .ReturnsAsync((Player?)null);
            var result = await this.cardCollectionService.FindPlayerCollection(Guid.NewGuid());
            Assert.Null(result);
        }
    }
}