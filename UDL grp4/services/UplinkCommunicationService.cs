using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ground_station.Models;

namespace ground_station.Services
{
    public class UplinkCommunicationService
    {
        private readonly ConcurrentQueue<CommandPacket> _commandQueue = new ConcurrentQueue<CommandPacket>();
        private readonly ILogger<UplinkCommunicationService> _logger;
        private readonly LoggingAndMonitoringService _loggingService;

        public UplinkCommunicationService(LoggingAndMonitoringService loggingService, ILogger<UplinkCommunicationService> logger)
        {
            _loggingService = loggingService;
            _logger = logger;
        }

        public void SendCommand(CommandPacket commandPacket)
        {
            var commandId = Guid.NewGuid();

            // Validate commandPacket
            if (commandPacket == null)
            {
                _loggingService.LogInfo($"[{commandId}] Command packet is null.");
                return;
            }

            if (string.IsNullOrWhiteSpace(commandPacket.Command))
            {
                _logger.LogWarning("[{CommandId}] Command is null or empty.", commandId);
                return;
            }

            if (commandPacket.Parameters == null || commandPacket.Parameters.Count == 0)
            {
                _logger.LogError("[{CommandId}] Error: Parameters cannot be null or empty.", commandId); // Updated log message
                return;
            }

            _logger.LogInformation("[{CommandId}] Sending command from {Source} to {Destination}: {Command} with parameters: {Parameters}", commandId, commandPacket.Source, commandPacket.Destination, commandPacket.Command, string.Join(", ", commandPacket.Parameters));
            _commandQueue.Enqueue(commandPacket);
            _logger.LogInformation("[{CommandId}] Command {Command} added to the queue for processing. Queue count: {Count}", commandId, commandPacket.Command, _commandQueue.Count);
        }

        public IEnumerable<CommandPacket> GetAllCommands()
        {
            _logger.LogInformation($"Retrieving all commands. Current queue count: {_commandQueue.Count}");
            return _commandQueue.ToArray();
        }
    }
}
