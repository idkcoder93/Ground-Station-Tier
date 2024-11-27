using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace Proxy.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ReceiveSendController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ReceiveSendController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("transfer")]
        
        public async Task<IActionResult> SpacecraftToSciOps(SpacecraftPayload spacecraftPayload)
        {

            {
                var sciops_base_url = "http://scientificoperationscenter.api:8000"; // URL of the Scientific Operations API

                // Step 1: Authenticate with SciOps
                var groundControlServiceAccount = new UserLogin
                {
                    UserName = "ground_control_sa",
                    Password = "ExploreSpace223*"
                };

                var authResponse = await _httpClient.PostAsJsonAsync($"{sciops_base_url}/auth/login", groundControlServiceAccount);

                if (!authResponse.IsSuccessStatusCode)
                    return Unauthorized("Failed to authenticate with Scientific Operations.");

                var sciOpsTokenResponse = await authResponse.Content.ReadFromJsonAsync<TokenResponse>();
                if (sciOpsTokenResponse == null || string.IsNullOrEmpty(sciOpsTokenResponse.Token))
                    return Unauthorized("Invalid authentication token received.");

                //// Step 2: Create a hard-coded command packet
                //var spacecraftPayload = new SpacecraftPayload
                //{
                //    DateTime = "2027-11-21 02:02:03",
                //    DataType = "RadiationReading",
                //    Data = "120",
                //    CRC = "A1B2C3D4"
                //};

                // Step 3: Send payload to SciOps
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{sciops_base_url}/api/receive")
                {
                    Content = JsonContent.Create(spacecraftPayload)
                };
                requestMessage.Headers.Add("Authorization", $"Bearer {sciOpsTokenResponse.Token}");
                //requestMessage.Headers.Add("Content-Type", "application/json");

                var sendResponse = await _httpClient.SendAsync(requestMessage);

                if (sendResponse.IsSuccessStatusCode)
                    return Ok("Payload successfully sent to Scientific Operations.");
                else
                    return StatusCode((int)sendResponse.StatusCode, "Failed to send payload to Scientific Operations.");

            }
        }

        public class SpacecraftPayload
        {
            [JsonProperty("dateTime")]
            public string DateTime { get; set; }

            [Required]
            [JsonProperty("dataType")]
            public string DataType { get; set; }

            [Required]
            [JsonProperty("data")]
            public string Data { get; set; }

            [JsonProperty("crc")]
            public string? CRC { get; set; }
        }

        public class UserLogin
        {
            [JsonProperty("username")]
            public string UserName { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }
        }

        public class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}