using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayerAuthServer.Core.Services;
using PlayerAuthServer.Database.Repositories;

namespace PlayerAuthServer.Core.Controllers
{
    [ApiController]
    [Route("api/player")]
    public class PlayerController(IPlayerService playerService, IPlayerRepository playerRepository) : ControllerBase
    {
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetPlayerProfile()
        {
            var idClaim = User.FindFirst("Id")?.Value;
            if (Guid.TryParse(idClaim, out var playerId))
            {
                var player = await playerRepository.FindPlayer(playerId);
                return player != null ? Ok(player) : NotFound();
            }

            return Unauthorized();
        }
    }
}
