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
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(destinationUrl))
            {
                _logger.LogError("Destination URL cannot be null or empty.");
                return false;
            }

            if (data == null)
            {
                _logger.LogError("Data cannot be null.");
                return false;
            }

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
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP request failed while forwarding data to {destinationUrl}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while forwarding data to {destinationUrl}: {ex.Message}");
                return false;
            }
        }
    }
}
