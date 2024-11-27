using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Dashboard
{
    class NetworkConnection
    {
        private static readonly HttpClient client = new HttpClient();

        private readonly string clientUrl = "http://localhost:5001/api/send";

        // Sends an HTTP POST request with JSON data
        private readonly Dictionary<string, string> loginInfo = new()
        {
            { "username", "user1" },
            { "password", "password1" }
        };
        public async Task<string?> SendAuthenticationRequestAsync()
        {
            try
            {
                // Base URL of the API
                string baseUrl = "http://localhost:5001/api/Authenticator/login";

                // Serialize the login information as JSON
                var jsonPayload = JsonSerializer.Serialize(loginInfo);

                // Create the content for the POST request
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await client.PostAsync(baseUrl, content);

                // Ensure the response status is successful
                response.EnsureSuccessStatusCode();

                // Read and return the response content
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // Display error
                MessageBox.Show("Message send error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public async Task<string?> SendHttpRequestAsync(string json)
        {
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(clientUrl, content);
                response.EnsureSuccessStatusCode(); // Throws if the status code is not successful
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // Display error
                MessageBox.Show("Message send error:", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private string ConvertJsonToUrl(string baseUrl, Dictionary<string, string> json)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            foreach (var kvp in json)
            {
                query[kvp.Key] = kvp.Value;
            }

            return $"{baseUrl}?{query}";
        }

        // Sends an HTTP GET request and returns the response
        public async Task<string?> ReceiveHttpRequestAsync()
        {
            try
            {
                var response = await client.GetStringAsync("http://localhost:9000/recepticle.aspx");
                return response;
            }
            catch (Exception ex)
            {
                // Display error
                MessageBox.Show("Message receiving error:", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
    }
}
