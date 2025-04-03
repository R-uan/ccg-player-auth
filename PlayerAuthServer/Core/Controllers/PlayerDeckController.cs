using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayerAuthServer.Core.Interfaces;

namespace PlayerAuthServer.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerDeckController(IPlayerDeckService playerDeckService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AssociatePlayerDeck([FromBody] string deckUUID)
        {
            if (Guid.TryParse(deckUUID, out var dUUID))
            {
                var playerUUIDClaim = User.FindFirst("UUID")?.Value;
                if (Guid.TryParse(playerUUIDClaim, out var pUUID))
                {
                    var association = await playerDeckService.LinkPlayerDeck(pUUID, dUUID);
                    return Ok(association);
                }
                else return Unauthorized("Unable to get claim from token");
            }
            else return BadRequest("Unable to parse deck uuid");

        }
    }
}
