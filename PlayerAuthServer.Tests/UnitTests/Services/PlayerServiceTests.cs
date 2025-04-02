using System;
using AutoMapper;
using Moq;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Core.Services;
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Utilities.DataTransferObjects;
using PlayerAuthServer.Utilities.Exceptions;

namespace PlayerAuthServer.Tests.UnitTests.Services
{
    public class PlayerServiceTests
    {
        private readonly Mock<IMapper> mockMapper;
        private readonly IPlayerService playerService;
        private readonly Mock<IPlayerRepository> mockPlayerRepository;

        public PlayerServiceTests()
        {
            this.mockMapper = new Mock<IMapper>();
            this.mockPlayerRepository = new Mock<IPlayerRepository>();
            this.playerService = new PlayerService(this.mockPlayerRepository.Object, this.mockMapper.Object);
        }

        [Fact]
        public async Task ShouldCreatePlayer()
        {
            Player player = new()
            {
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = "hash1",
                Decks = []
            };

            PlayerDto playerDto = new()
            {
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = "hash1",
            };

            // Setup Mapper
            mockMapper.Setup(mapper => mapper.Map<Player>(It.IsAny<PlayerDto>())).Returns(player);
            mockMapper.Setup(mapper => mapper.Map<PlayerDto>(It.IsAny<Player>())).Returns(playerDto);

            // Setup Repository
            mockPlayerRepository.Setup(repository => repository.FindPlayerByEmail(It.IsAny<string>()))
                .ReturnsAsync((Player?)null);
            mockPlayerRepository.Setup(repository => repository.FindPlayerByNickname(It.IsAny<string>()))
                .ReturnsAsync((Player?)null);
            mockPlayerRepository.Setup(repository => repository.CreatePlayer(player))
                .ReturnsAsync((Player)player);

            var result = await this.playerService.CreatePlayer(playerDto);

            mockPlayerRepository.Verify(repository => repository.CreatePlayer(It.IsAny<Player>()), Times.Once);
            mockPlayerRepository.Verify(repository => repository.FindPlayerByEmail(It.IsAny<string>()), Times.Once);
            mockPlayerRepository.Verify(repository => repository.FindPlayerByNickname(It.IsAny<string>()), Times.Once);

            mockMapper.Verify(mapper => mapper.Map<Player>(It.IsAny<PlayerDto>()), Times.Once);
            mockMapper.Verify(mapper => mapper.Map<PlayerDto>(It.IsAny<Player>()), Times.Once);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldThrowDuplicateEmailException()
        {
            Player player = new()
            {
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = "hash1",
                Decks = []
            };

            PlayerDto playerDto = new()
            {
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = "hash1",
            };

            // Setup Mapper
            mockMapper.Setup(mapper => mapper.Map<Player>(It.IsAny<PlayerDto>())).Returns(player);
            mockMapper.Setup(mapper => mapper.Map<PlayerDto>(It.IsAny<Player>())).Returns(playerDto);

            // Setup Repository
            mockPlayerRepository.Setup(repository => repository.FindPlayerByEmail(It.IsAny<string>()))
                .ReturnsAsync((Player?)player);
            mockPlayerRepository.Setup(repository => repository.FindPlayerByNickname(It.IsAny<string>()))
                .ReturnsAsync((Player?)null);

            var result = await Assert.ThrowsAsync<DuplicateEmailException>(
                async () => await this.playerService.CreatePlayer(playerDto)
            );

            mockPlayerRepository.Verify(repository => repository.FindPlayerByEmail(It.IsAny<string>()), Times.Once);
            Assert.IsType<DuplicateEmailException>(result);
        }

        [Fact]
        public async Task ShouldThrowDuplicateNicknameException()
        {
            Player player = new()
            {
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = "hash1",
                Decks = []
            };

            PlayerDto playerDto = new()
            {
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = "hash1",
            };

            // Setup Mapper
            mockMapper.Setup(mapper => mapper.Map<Player>(It.IsAny<PlayerDto>())).Returns(player);
            mockMapper.Setup(mapper => mapper.Map<PlayerDto>(It.IsAny<Player>())).Returns(playerDto);

            // Setup Repository
            mockPlayerRepository.Setup(repository => repository.FindPlayerByEmail(It.IsAny<string>()))
                .ReturnsAsync((Player?)null);
            mockPlayerRepository.Setup(repository => repository.FindPlayerByNickname(It.IsAny<string>()))
                .ReturnsAsync((Player?)player);

            var result = await Assert.ThrowsAsync<DuplicateNicknameException>(
                async () => await this.playerService.CreatePlayer(playerDto)
            );

            mockPlayerRepository.Verify(repository => repository.FindPlayerByEmail(It.IsAny<string>()), Times.Once);
            Assert.IsType<DuplicateNicknameException>(result);
        }

        [Fact]
        public async Task ShouldReturnPlayerEntityByGuid()
        {
            Player player = new()
            {
                UUID = Guid.NewGuid(),
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = "hash1",
                Decks = []
            };

            mockPlayerRepository.Setup(repository => repository.FindPlayer(It.IsAny<Guid>()))
                .ReturnsAsync((Player?)player);

            var result = await this.playerService.FindPlayer(player.UUID);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldReturnNullByGuid()
        {
            mockPlayerRepository.Setup(repository => repository.FindPlayer(It.IsAny<Guid>()))
                .ReturnsAsync((Player?)null);

            var result = await this.playerService.FindPlayer(Guid.NewGuid());
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldReturnPlayerEntityByEmail()
        {
            Player player = new()
            {
                UUID = Guid.NewGuid(),
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = "hash1",
                Decks = []
            };

            mockPlayerRepository.Setup(repository => repository.FindPlayerByEmail(It.IsAny<string>()))
                .ReturnsAsync((Player?)player);

            var result = await this.playerService.FindPlayerByEmail(player.Email);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldReturnNullByEmail()
        {
            mockPlayerRepository.Setup(repository => repository.FindPlayerByEmail(It.IsAny<string>()))
                .ReturnsAsync((Player?)null);

            var result = await this.playerService.FindPlayerByEmail("player1@example.com");
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldReturnPlayerEntityByNickname()
        {
            Player player = new()
            {
                UUID = Guid.NewGuid(),
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = "hash1",
                Decks = []
            };

            mockPlayerRepository.Setup(repository => repository.FindPlayerByNickname(It.IsAny<string>()))
                .ReturnsAsync((Player?)player);

            var result = await this.playerService.FindPlayerByNickname(player.Nickname);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldReturnNullByNickname()
        {
            mockPlayerRepository.Setup(repository => repository.FindPlayerByNickname(It.IsAny<string>()))
                .ReturnsAsync((Player?)null);

            var result = await this.playerService.FindPlayerByNickname("PlayerOne");
            Assert.Null(result);
        }
    }
}
