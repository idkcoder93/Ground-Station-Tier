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

        public virtual void LogInfo(string message) // Make this virtual
        {
            _logger.LogInformation(message);
        }

        public virtual void LogError(string message) // Make this virtual
        {
            _logger.LogError(message);
        }
    }
}

