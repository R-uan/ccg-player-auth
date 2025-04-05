using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Utilities.Requests;

namespace PlayerAuthServer.Core.Controllers
{
    [ApiController]
    [Route("api/deck")]
    public class PlayerDeckController(IPlayerDeckService playerDeckService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AssociatePlayerDeck([FromBody] LinkDeckRequest request)
        {
            var playerUUIDClaim = User.FindFirst("UUID")?.Value;
            if (Guid.TryParse(playerUUIDClaim, out var pUUID))
            {
                var association = await playerDeckService.LinkPlayerDeck(pUUID, request.DeckUUID);
                return Ok(association);
            }
            else return Unauthorized("Unable to get claim from token");
        }
    }
}
