using Microsoft.AspNetCore.Mvc;
using PlayerAuthServer.Database.DataTransferObject;
using PlayerAuthServer.Exceptions;
using PlayerAuthServer.Interfaces;

namespace PlayerAuthServer.Controller
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService service) : ControllerBase
    {
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                string jwt = await service.AuthenticatePlayer(login);
                return Ok(jwt);

            }
            catch (System.Exception exception)
            {
                return exception switch
                {
                    UnauthorizedAccessException => Unauthorized(exception.Message),
                    PlayerNotFoundException => NotFound(exception.Message),
                    _ => StatusCode(500, exception.Message)
                };
                throw;
            }
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto player)
        {
            try
            {
                var register = await service.RegisterPlayer(player);
                return Ok(register);
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