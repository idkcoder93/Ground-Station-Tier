using ground_station.Services;

public class DataForwardingService
{
    private readonly HttpClient _httpClient;
    private readonly MongoDbService _mongoDbService;
    private readonly ILogger<DataForwardingService> _logger;

    public DataForwardingService(HttpClient httpClient, MongoDbService mongoDbService, ILogger<DataForwardingService> logger)
    {
        _httpClient = httpClient;
        _mongoDbService = mongoDbService;
        _logger = logger;
    }

    public async Task<bool> ForwardDataAsync(string url, object data)
    {
        try
        {
            // Log full URL and method
            _logger.LogInformation($"Sending POST request to {url}");

            var response = await _httpClient.PostAsJsonAsync(url, data);

            // Log response status and content
            _logger.LogInformation($"Response Status: {response.StatusCode}");
            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error Response: {responseBody}");
            }

            response.EnsureSuccessStatusCode();  // This throws if status code is not 2xx
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            // Log more detailed error information
            _logger.LogError($"Error forwarding data to {url}: {ex.Message}");
            _logger.LogError($"Response: {ex.InnerException?.Message}");
            return false;
        }
    }
}
