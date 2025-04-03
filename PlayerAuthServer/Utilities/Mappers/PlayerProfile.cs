using AutoMapper;
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Utilities.DataTransferObjects;
using PlayerAuthServer.Utilities.Requests;

namespace PlayerAuthServer.Utilities.Mappers
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<RegisterRequest, PlayerDto>().ReverseMap();
        }
    }
}
