using AutoMapper;
using PlayerAuthServer.Entities;
using PlayerAuthServer.Entities.Models;
using PlayerAuthServer.Utilities.Requests;

namespace PlayerAuthServer.Utilities.Mappers
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerDto>();
            CreateMap<RegisterRequest, NewPlayer>()
                .ForMember(
                    dest => dest.PasswordHash,
                    opt => opt.MapFrom(src => Bcrypt.HashPassword(src.Password))
                );
        }
    }
}
