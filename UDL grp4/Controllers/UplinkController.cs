using Microsoft.AspNetCore.Mvc;
using ground_station.Models;
using ground_station.Services;

namespace ground_station.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UplinkController : ControllerBase
    {
        private readonly UplinkCommunicationService _uplinkService;

        public UplinkController(UplinkCommunicationService uplinkService)
        {
            _uplinkService = uplinkService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CommandPacket commandPacket)
        {
            if (commandPacket == null || !commandPacket.IsValid)
            {
                return BadRequest("Invalid command packet.");
            }

            _uplinkService.SendCommand(commandPacket);
            return Ok(new { Message = "Data sent successfully." });
        }

        [HttpGet]
        public IActionResult Get()
        {
            var commands = _uplinkService.GetAllCommands();
            return Ok(commands);
        }
    }
}
