using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayerAuthServer.Interfaces;
using PlayerAuthServer.Utilities.Requests;

namespace PlayerAuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardCollectionController(ICardCollectionService cardCollectionService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostCardCollection([FromBody] CollectCardRequest request)
        {
            var userIdClaim = User.FindFirst("Id")?.Value;
            if (Guid.TryParse(userIdClaim, out var playerId))
            {
                var cardCollection = await cardCollectionService.CollectCard(request, playerId);
                return Ok(cardCollection);
            }

            return Unauthorized();
        }
    }
}
