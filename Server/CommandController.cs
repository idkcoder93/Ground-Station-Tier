using Microsoft.AspNetCore.Mvc;
using Server.Models; // Ensure you have the correct namespace for the CommandPacket model

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandController : ControllerBase
    {
        // POST endpoint to accept JSON data from the client
        [HttpPost("receive-packet")]
        public IActionResult ReceivePacket([FromBody] CommandPacket commandPacket)
        {
            if (commandPacket == null)
            {
                return BadRequest("Invalid packet data.");
            }

            // Log or process the received packet data here (can be expanded)
            Console.WriteLine($"Received data: {commandPacket.SensorData?.Temperature}, {commandPacket.SensorData?.Humidity}, {commandPacket.Settings?.PowerSetting}");

            // Send a simple confirmation response
            return Ok("Connection successful! Data received.");
        }
    }
}
