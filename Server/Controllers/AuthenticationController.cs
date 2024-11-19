using Microsoft.AspNetCore.Mvc;

namespace GroundStationServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        // POST: api/authentication/login
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Invalid request." });
            }

            // Example authentication logic
            if (request.Username == "user" && request.Password == "pass")
            {
                return Ok(new { message = "Authentication successful." });
            }
            else
            {
                return Unauthorized(new { message = "Authentication failed." });
            }
        }
    }


    // Model for the incoming authentication request
    public class AuthenticationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
