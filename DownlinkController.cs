using Microsoft.AspNetCore.Mvc;
using ground_station.Models; // Ensure you have the correct namespace
using ground_station.Services; // Assuming you have your services in this namespace

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

        // GET api/downlink
        [HttpGet]
        public ActionResult<string> GetData()
        {
            // Implement logic to retrieve downlink data if needed
            return Ok("Downlink data retrieved successfully.");
        }

        // You may add POST method if you want to handle incoming packets
        // POST api/downlink
        [HttpPost]
        public ActionResult<string> PostData([FromBody] string data)
        {
            // Logic to process downlink data
            return Ok("Data received successfully.");
        }
    }
}
