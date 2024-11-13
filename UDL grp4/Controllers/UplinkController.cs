using Microsoft.AspNetCore.Mvc;
using ground_station.Models;
using ground_station.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ground_station.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UplinkController : ControllerBase
    {
        private readonly DataForwardingService _dataForwardingService;

        // Dictionary to map each destination to a specific URL
        private readonly Dictionary<string, string> _destinationUrls = new Dictionary<string, string>
        {
            { "ScientificOperation", "http://localhost:/api/receive" },    // Add port number for scientific service
            { "PayloadOps", "http://localhost:5297/api/receive" },        // add port number for pay
            { "Spacecraft", "http://localhost:/api/receive" }                // add port number for space
        };

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

            if (string.IsNullOrEmpty(commandPacket.Destination) || !_destinationUrls.TryGetValue(commandPacket.Destination, out var destinationUrl))
            {
                return BadRequest("Invalid or unknown destination.");
            }

            // Forward the data to the appropriate service based on the destination
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
