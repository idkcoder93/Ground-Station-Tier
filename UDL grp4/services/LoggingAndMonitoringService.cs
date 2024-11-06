using Microsoft.Extensions.Logging;
using System;

namespace ground_station.Services
{
    public class LoggingAndMonitoringService
    {
        private readonly ILogger<LoggingAndMonitoringService> _logger;

        public LoggingAndMonitoringService(ILogger<LoggingAndMonitoringService> logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogError(string message)
        {
            _logger.LogError(message);
        }
    }
}
