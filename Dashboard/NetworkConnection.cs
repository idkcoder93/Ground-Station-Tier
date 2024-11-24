using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard
{
    class NetworkConnection
    {
        private static readonly HttpClient client = new HttpClient();

        // Sends an HTTP POST request with JSON data
        public async Task<string?> SendHttpRequestAsync(string json)
        {
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:5102", content);
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
