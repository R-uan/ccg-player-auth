
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Utilities.DataTransferObjects;
using PlayerAuthServer.Utilities.Exceptions;

namespace PlayerAuthServer.Core.Services
{
    public class AuthService(JwtService jwtService, IPlayerService playerService) : IAuthService
    {
        public async Task<string> AuthenticatePlayer(LoginDto credentials)
        {
            Player? player = await playerService.FindPlayerByEmail(credentials.Email)
                ?? throw new PlayerNotFoundException("Player email was not found");
            if (!Bcrypt.Verify(credentials.Password, player.PasswordHash))
                throw new UnauthorizedAccessException("");
            return jwtService.GenerateToken(player);
        }

        public async Task<PlayerDto> RegisterPlayer(RegisterDto player)
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
