using AutoMapper;
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Utilities.DataTransferObjects;

namespace PlayerAuthServer.Utilities.Mappers
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<RegisterDto, PlayerDto>().ReverseMap();
        }
    }
}
