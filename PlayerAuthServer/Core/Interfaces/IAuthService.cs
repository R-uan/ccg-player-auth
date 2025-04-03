using PlayerAuthServer.Utilities.DataTransferObjects;
using PlayerAuthServer.Utilities.Requests;

namespace PlayerAuthServer.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticatePlayer(LoginRequest player);
        Task<PlayerDto> RegisterPlayer(RegisterRequest player);
    }

}