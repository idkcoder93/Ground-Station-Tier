using Microsoft.AspNetCore.Mvc;
using ground_station.Models;
using ground_station.Services;
using System.Threading.Tasks;

namespace ground_station.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UplinkController : ControllerBase
    {
        private readonly DataForwardingService _dataForwardingService;

        public UplinkController(DataForwardingService dataForwardingService)
        {
            _dataForwardingService = dataForwardingService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommandPacket commandPacket)
        {
            if (commandPacket == null)
            {
                return BadRequest("Invalid command packet.");
            }

            // Forward the data to another service
            var destinationUrl = ""; // Replace with the actual URL of the destination service
            var forwardSuccess = await _dataForwardingService.ForwardDataAsync(destinationUrl, commandPacket);

            if (forwardSuccess)
            {
                return Ok(new { Message = "Uplink data sent and forwarded successfully." });
            }
            else
            {
                return StatusCode(500, new { Message = "Failed to forward uplink data." });
            }
        }
    }
}
