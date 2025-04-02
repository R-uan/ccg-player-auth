using PlayerAuthServer.Utilities.DataTransferObjects;

namespace PlayerAuthServer.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticatePlayer(LoginDto player);
        Task<PlayerDto> RegisterPlayer(RegisterDto player);
    }

}