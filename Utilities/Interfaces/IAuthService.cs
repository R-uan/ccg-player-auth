using System;
using PlayerAuthServer.Database.DataTransferObject;

namespace PlayerAuthServer.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticatePlayer(LoginDto player);
        Task<PlayerDto> RegisterPlayer(RegisterDto player);
    }

}