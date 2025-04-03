using System;
using AutoMapper.Configuration.Annotations;
using Moq;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Core.Services;
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Utilities.DataTransferObjects;
using PlayerAuthServer.Utilities.Exceptions;
using PlayerAuthServer.Utilities.Requests;

namespace PlayerAuthServer.Tests.UnitTests.Services
{
    public class AuthServiceTests
    {
        private readonly AuthService authService;
        private readonly Mock<IJwtService> mockJwtService;
        private readonly Mock<IPlayerService> mockPlayerService;

        public AuthServiceTests()
        {
            this.mockJwtService = new Mock<IJwtService>();
            this.mockPlayerService = new Mock<IPlayerService>();
            this.authService = new AuthService(this.mockJwtService.Object, this.mockPlayerService.Object);
        }

        [Fact]
        public async Task ShouldReturnToken()
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("hash1");
            LoginRequest login = new()
            {
                Email = "player1@example.com",
                Password = "hash1"
            };

            Player player = new()
            {
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = hashedPassword,
                Decks = []
            };

            this.mockPlayerService.Setup(service => service.FindPlayerByEmail(login.Email))
                .ReturnsAsync((Player?)player);
            this.mockJwtService.Setup(service => service.GenerateToken(player))
                .Returns("It.IsAny<string>()");

            string result = await authService.AuthenticatePlayer(login);

            this.mockPlayerService.Verify(service => service.FindPlayerByEmail(It.IsAny<string>()), Times.Once);
            this.mockJwtService.Verify(service => service.GenerateToken(It.IsAny<Player>()), Times.Once);

            Console.WriteLine($">> {result}");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task ShouldThrowEmailNotFoundException()
        {
            LoginRequest login = new()
            {
                Email = "player1@example.com",
                Password = "hash1"
            };

            this.mockPlayerService.Setup(service => service.FindPlayerByEmail(login.Email))
                .ReturnsAsync((Player?)null);

            var result = await Assert.ThrowsAsync<PlayerNotFoundException>(
                async () => await this.authService.AuthenticatePlayer(login)
            );

            Assert.IsType<PlayerNotFoundException>(result);
        }


        [Fact]
        public async Task ShouldThrowUnauthorizedAccessException()
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("hash1");
            LoginRequest login = new()
            {
                Email = "player1@example.com",
                Password = "hash2"
            };

            Player player = new()
            {
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = hashedPassword,
                Decks = []
            };

            this.mockPlayerService.Setup(service => service.FindPlayerByEmail(login.Email))
                .ReturnsAsync((Player?)player);

            var result = await Assert.ThrowsAsync<UnauthorizedAccessException>(
                async () => await authService.AuthenticatePlayer(login)
            );

            this.mockPlayerService.Verify(service => service.FindPlayerByEmail(It.IsAny<string>()), Times.Once);
            Assert.IsType<UnauthorizedAccessException>(result);
        }

        [Fact]
        public async Task ShouldReturnPlayerDto()
        {
            PlayerDto playerDto = new()
            {
                Email = "email@gmail.com",
                Nickname = "hotmail",
                PasswordHash = "protonmail",
            };

            RegisterRequest registerDto = new()
            {
                Email = "email@gmail.com",
                Nickname = "hotmail",
                Password = "protonmail"
            };

            this.mockPlayerService.Setup(service => service.CreatePlayer(It.IsAny<PlayerDto>()))
                .ReturnsAsync(playerDto);

            var result = await this.authService.RegisterPlayer(registerDto);

            Assert.NotNull(result);

        }
    }
}
