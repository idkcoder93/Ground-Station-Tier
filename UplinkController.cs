using Microsoft.AspNetCore.Mvc;
using ground_station.Models; // Ensure you have the correct namespace
using ground_station.Services; // Assuming you have your services in this namespace
using Microsoft.Extensions.Logging; // Add this for logging

namespace ground_station.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UplinkController : ControllerBase
    {
        private readonly UplinkCommunicationService _uplinkService;
        private readonly ILogger<UplinkController> _logger;

        public UplinkController(UplinkCommunicationService uplinkService, ILogger<UplinkController> logger)
        {
            _uplinkService = uplinkService;
            _logger = logger;
        }

        // POST api/uplink
        [HttpPost]
        public ActionResult<ApiResponse<CommandPacket>> PostCommand([FromBody] CommandPacket commandPacket)
        {
            if (commandPacket == null)
            {
                _logger.LogWarning("Received null command packet");
                return BadRequest("Invalid command packet.");
            }

            _logger.LogInformation($"Received command: {commandPacket.Command}, Parameters: {commandPacket.Parameters}");

            try
            {
                _uplinkService.SendCommand(commandPacket);
                return Ok(new ApiResponse<CommandPacket>
                {
                    Success = true,
                    Data = commandPacket,
                    Message = "Command packet sent successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending command packet");
                return StatusCode(500, new ApiResponse<CommandPacket>
                {
                    Success = false,
                    Data = null!,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }

        // GET api/uplink
        [HttpGet]
        public ActionResult<ApiResponse<string>> GetStatus()
        {
            _logger.LogInformation("GetStatus called");
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Data = "Uplink service is operational.",
                Message = "Status retrieved successfully."
            });
        }
    }
}
