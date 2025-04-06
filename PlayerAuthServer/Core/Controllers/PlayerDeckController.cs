using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PlayerAuthServer.Utilities.Requests;
using PlayerAuthServer.Core.Services;

namespace PlayerAuthServer.Core.Controllers
{
    [ApiController]
    [Route("api/deck")]
    public class PlayerDeckController(IPlayerDeckService playerDeckService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RegisterPlayerDeck([FromBody] LinkDeckRequest request)
        {
            var playerIdClaim = User.FindFirst("Id")?.Value;
            if (Guid.TryParse(playerIdClaim, out var playerId))
                return Ok(await playerDeckService.LinkPlayerDeckAsync(playerId, request.DeckId));
            else return Unauthorized("Unable to get claim from token");
        }
    }
}
