using AutoMapper;
using PlayerAuthServer.Utilities.Requests;
using PlayerAuthServer.Utilities.Exceptions;
using PlayerAuthServer.Entities.Models;
using PlayerAuthServer.Database.Repositories;

namespace PlayerAuthServer.Core.Services
{
    public class AuthService(
        IMapper mapper,
        IJwtService jwtService,
        IPlayerService playerService,
        IPlayerRepository playerRepository) : IAuthService
    {
        public async Task<string> AuthenticatePlayer(LoginRequest credentials)
        {
            var player = await playerRepository.FindPlayerByEmail(credentials.Email)
                ?? throw new PlayerNotFoundException("Invalid credentials.");

            bool passwordValid = Bcrypt.Verify(credentials.Password, player.PasswordHash);

            if (!passwordValid)
                throw new UnauthorizedAccessException("Invalid credentials.");

            return jwtService.GenerateToken(player);
        }

        public async Task<PlayerDto> RegisterNewPlayer(NewPlayer newPlayer)
        {
            if (await playerRepository.FindPlayerByEmail(newPlayer.Email) is not null)
                throw new DuplicateEmailException("Email already registered");

            if (await playerRepository.FindPlayerByUsername(newPlayer.Username) is not null)
                throw new DuplicateUsernameException(newPlayer.Username);

            var savedPlayer = await playerService.CreatePlayerWithDefaults(newPlayer);
            return mapper.Map<PlayerDto>(savedPlayer);
        }
    }
}
