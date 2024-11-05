using Microsoft.AspNetCore.Mvc;
using ground_station.Models;
using ground_station.Services;

namespace ground_station.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownlinkController : ControllerBase
    {
        private readonly DownlinkCommunicationService _downlinkService;

        public DownlinkController(DownlinkCommunicationService downlinkService)
        {
            _downlinkService = downlinkService;
        }

        // POST api/downlink/receive
        [HttpPost("receive")]
        public ActionResult<string> ReceiveCommand([FromBody] CommandPacket commandPacket)
        {
            _downlinkService.ReceiveCommand(commandPacket);
            return Ok("Command received successfully.");
        }

        // GET api/downlink/commands
        [HttpGet("commands")]
        public ActionResult<IEnumerable<CommandPacket>> GetAllCommands()
        {
            var commands = _downlinkService.GetAllReceivedCommands();
            return Ok(commands);
        }

        // GET api/downlink/next
        [HttpGet("next")]
        public ActionResult<CommandPacket?> GetNextCommand()
        {
            var command = _downlinkService.GetNextReceivedCommand();
            return command != null ? Ok(command) : NotFound("No commands available to process.");
        }
    }
}
