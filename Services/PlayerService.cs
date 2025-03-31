using AutoMapper;
using PlayerAuthServer.Database.DataTransferObject;
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Exceptions;
using PlayerAuthServer.Interfaces;
namespace PlayerAuthServer.Services
{
    public class PlayerService(IPlayerRepository repository, IMapper _mapper) : IPlayerService
    {
        public async Task<bool> CreatePlayer(PlayerDto playerDto)
        {
            if (!(await repository.FindPlayerByEmail(playerDto.Email) == null))
                throw new DuplicateEmailException(playerDto.Email);
            if (!(await repository.FindPlayerByNickname(playerDto.Nickname) == null))
                throw new DuplicateNicknameException(playerDto.Nickname);

            var player = _mapper.Map<Player>(playerDto);
            var result = await repository.CreatePlayer(player);
            return result != null;
        }

        public async Task<Player?> FindPlayer(Guid uuid)
            => await repository.FindPlayer(uuid);
    }
}
