using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayerAuthServer.Core.Services;
using PlayerAuthServer.Utilities.Requests;
using PlayerAuthServer.Utilities.Responses;
using PlayerAuthServer.Utilities.Exceptions;
using PlayerAuthServer.Entities.Models;

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

        /// <summary>
        /// Handles player registration requests.
        /// Validates input and delegates to the player service to create a new account.
        /// Returns appropriate error messages for duplicate usernames or emails.
        /// </summary>
        /// <param name="request">The registration data submitted by the client.</param>
        /// <returns>An <see cref="IActionResult"/> indicating success or failure.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterPlayer([FromBody] RegisterRequest request)
        {
            try
            {
                var registeredPlayer = await service.RegisterNewPlayer(request);
                var response = new RegisterResponse(registeredPlayer);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex is DuplicateEmailException || ex is DuplicateUsernameException)
                    return BadRequest(ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}