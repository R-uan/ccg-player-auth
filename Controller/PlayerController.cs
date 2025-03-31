using Microsoft.AspNetCore.Mvc;
using PlayerAuthServer.Interfaces;
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Database.DataTransferObject;

namespace PlayerAuthServer.Controller
{
    [ApiController]
    [Route("api/player")]
    public class PlayerController(IPlayerService service) : ControllerBase
    {
        [HttpGet("{uuid}")]
        public async Task<ActionResult<Player?>> GetPlayer(string uuid)
        {
            if (Guid.TryParse(uuid, out var playerGuid))
            {
                Player? player = await service.FindPlayer(playerGuid);
                return player != null ? Ok(player) : NotFound("Player not found");
            }

            return BadRequest("Could not parse GUID");
        }

        [HttpPost("")]
        public async Task<ActionResult<bool>> PostPlayer([FromBody] PlayerDto player)
        {
            var create_player = await service.CreatePlayer(player);
            if (!ModelState.IsValid)
            {
                return BadRequest("xdx");
            }
            return create_player ? Ok() : BadRequest("aaa");
        }
    }
}
