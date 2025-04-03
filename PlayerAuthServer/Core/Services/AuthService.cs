
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Utilities.DataTransferObjects;
using PlayerAuthServer.Utilities.Exceptions;
using PlayerAuthServer.Utilities.Requests;

namespace PlayerAuthServer.Core.Services
{
    public class AuthService(IJwtService jwtService, IPlayerService playerService) : IAuthService
    {
        public async Task<string> AuthenticatePlayer(LoginRequest credentials)
        {
            Player? player = await playerService.FindPlayerByEmail(credentials.Email)
                ?? throw new PlayerNotFoundException("Invalid credentials");
            if (!Bcrypt.Verify(credentials.Password, player.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");
            return jwtService.GenerateToken(player);
        }

        public async Task<PlayerDto> RegisterPlayer(RegisterRequest player)
        {
            var map = new PlayerDto
            {
                Email = player.Email,

                Nickname = player.Nickname,
                PasswordHash = Bcrypt.HashPassword(player.Password),
            };

            return await playerService.CreatePlayer(map);
        }
    }
}
