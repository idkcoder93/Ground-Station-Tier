using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ground_station.Services
{
    public class DataForwardingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DataForwardingService> _logger;

        public DataForwardingService(HttpClient httpClient, ILogger<DataForwardingService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> ForwardDataAsync(string destinationUrl, object data)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(destinationUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Data successfully forwarded to {destinationUrl}");
                    return true;
                }
                else
                {
                    _logger.LogError($"Failed to forward data to {destinationUrl}. Status code: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while forwarding data to {destinationUrl}: {ex.Message}");
                return false;
            }
        }
    }
}
