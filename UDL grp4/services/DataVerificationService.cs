using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using ground_station.Models;

namespace ground_station.Services
{
    public class DataVerificationService
    {
        private readonly ILogger<DataVerificationService> _logger;

        public DataVerificationService(ILogger<DataVerificationService> logger)
        {
            _logger = logger;
        }

        public bool VerifyCommandPacket(CommandPacket packet)
        {
            if (packet == null)
            {
                _logger.LogWarning("Command packet is null.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(packet.Command))
            {
                _logger.LogWarning("Command is null or empty.");
                return false;
            }

            if (!VerifyParameters(packet.Parameters))
            {
                _logger.LogWarning($"Parameters for command {packet.Command} are invalid.");
                return false;
            }

            _logger.LogInformation($"Command packet {packet.Command} verified successfully.");
            return true;
        }

        public bool VerifyParameters(Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                _logger.LogWarning("Parameters dictionary is null or empty.");
                return false;
            }

            foreach (var kvp in parameters)
            {
                if (string.IsNullOrWhiteSpace(kvp.Key) || string.IsNullOrWhiteSpace(kvp.Value))
                {
                    _logger.LogWarning($"Invalid parameter: Key='{kvp.Key}', Value='{kvp.Value}'");
                    return false;
                }
                // Add more specific validation logic for parameter values if needed
            }

            return true;
        }
    }
}
