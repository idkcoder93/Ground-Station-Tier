using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using GroundStationServer.Services;

namespace GroundStationServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroundStationController : ControllerBase
    {
        private static readonly LoggerService Logger = new LoggerService("ServerLogs.txt");

        /// <summary>
        /// Endpoint for user authentication.
        /// Stores session data upon successful authentication.
        /// </summary>
        /// <param name="credentials">Dictionary containing "username" and "password".</param>
        /// <returns>Success or failure response.</returns>
        [HttpPost("Authentication/login")]
        public IActionResult Authenticate([FromBody] Dictionary<string, string> credentials)
        {
            if (credentials.ContainsKey("username") && credentials["username"] == "user" &&
                credentials.ContainsKey("password") && credentials["password"] == "pass")
            {
                HttpContext.Session.SetString("UserId", credentials["username"]);
                Logger.Log($"Authentication SUCCESS for user: {credentials["username"]}");
                return Ok("Authentication successful");
            }

            Logger.Log("Authentication FAILED. Invalid credentials.");
            return Unauthorized("Authentication failed");
        }

        /// <summary>
        /// Endpoint to receive data updates from the client.
        /// Requires the user to be authenticated.
        /// </summary>
        /// <param name="data">Key-value pairs sent by the client.</param>
        /// <returns>Response indicating the server received the data.</returns>
        [HttpPost("Downlink")]
        public IActionResult ReceiveData([FromBody] Dictionary<string, string> data)
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                Logger.Log("Unauthorized request. Session not found.");
                return Unauthorized("User not authenticated.");
            }

            string receivedData = string.Join(", ", data.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
            Logger.Log($"Data Received from user {userId}: {receivedData}");
            return Ok("Data received successfully");
        }
    }
}
