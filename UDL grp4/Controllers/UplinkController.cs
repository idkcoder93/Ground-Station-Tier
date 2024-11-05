using Microsoft.AspNetCore.Mvc;
using ground_station.Models;
using ground_station.Services;
using System.Collections.Generic;

namespace ground_station.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UplinkController : ControllerBase
    {
        private readonly UplinkCommunicationService _uplinkService;

        public UplinkController(UplinkCommunicationService uplinkService)
        {
            _uplinkService = uplinkService;
        }

        [HttpPost]
        public ActionResult<ApiResponse<CommandPacket>> Post([FromBody] CommandPacket commandPacket)
        {
            if (commandPacket == null)
            {
                return BadRequest("Invalid command packet.");
            }

            _uplinkService.SendCommand(commandPacket);
            return Ok(new ApiResponse<CommandPacket>
            {
                Success = true,
                Data = commandPacket,
                Message = "Command sent successfully."
            });
        }

        [HttpGet("commands")]
        public ActionResult<ApiResponse<IEnumerable<CommandPacket>>> GetCommands()
        {
            var commands = _uplinkService.GetAllCommands();
            return Ok(new ApiResponse<IEnumerable<CommandPacket>>
            {
                Success = true,
                Data = commands,
                Message = "Commands retrieved successfully."
            });
        }
    }
}
