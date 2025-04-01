using System;
using AutoMapper;
using PlayerAuthServer.Database.DataTransferObject;
using PlayerAuthServer.Database.Entities;

namespace PlayerAuthServer.Mapper;

public class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<Player, PlayerDto>().ReverseMap();
        CreateMap<RegisterDto, PlayerDto>().ReverseMap();
    }
}
