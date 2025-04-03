using Microsoft.AspNetCore.Mvc;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Utilities.Exceptions;
using PlayerAuthServer.Utilities.DataTransferObjects;
using PlayerAuthServer.Utilities.Requests;
using PlayerAuthServer.Utilities.Responses;

namespace PlayerAuthServer.Core.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService service) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            try
            {
                string jwt = await service.AuthenticatePlayer(login);
                return Ok(new LoginResponse { Token = jwt });

            }
            catch (System.Exception exception)
            {
                return exception switch
                {
                    UnauthorizedAccessException => Unauthorized(exception.Message),
                    PlayerNotFoundException => Unauthorized(exception.Message),
                    _ => StatusCode(500, exception.Message)
                };
                throw;
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest player)
        {
            try
            {
                var register = await service.RegisterPlayer(player);
                return Ok(new RegisterResponse { Email = register.Email, Nickname = register.Nickname });
            }
            catch (System.Exception exception)
            {
                return exception switch
                {
                    DuplicateNicknameException => BadRequest(exception.Message),
                    DuplicateEmailException => BadRequest(exception.Message),
                    _ => StatusCode(500, exception.Message),
                };
            }
        }
    }
}