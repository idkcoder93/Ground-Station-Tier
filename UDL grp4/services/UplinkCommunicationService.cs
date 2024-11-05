using System;
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

        public UplinkCommunicationService(ILogger<UplinkCommunicationService> logger)
        {
            _logger = logger;
        }

        public void SendCommand(CommandPacket commandPacket)
        {
            var commandId = Guid.NewGuid();

            // Validate commandPacket
            if (commandPacket == null)
            {
                _logger.LogInformation($"[{commandId}] Command packet is null.");
                return;
            }

            if (string.IsNullOrWhiteSpace(commandPacket.Command))
            {
                _logger.LogInformation($"[{commandId}] Command is null or empty.");
                return;
            }

            if (commandPacket.Parameters == null)
            {
                _logger.LogInformation($"[{commandId}] Parameters are null for command: {commandPacket.Command}.");
                return;
            }

            // Log the command being sent
            _logger.LogInformation($"[{commandId}] Sending command: {commandPacket.Command} with parameters: {string.Join(", ", commandPacket.Parameters)}");

            // Enqueue the command for processing
            _commandQueue.Enqueue(commandPacket);

            // Log the queue status
            _logger.LogInformation($"[{commandId}] Command {commandPacket.Command} added to the queue for processing. Queue count: {_commandQueue.Count}");
        }

        public void ReceiveBadPacket(CommandPacket badPacket)
        {
            // Log the bad packet and handle it
            _logger.LogWarning($"Received bad packet: {badPacket.Command}. Sending back for correction.");
            // You can add logic here to process the bad packet as needed
            // For example, log, store, or inform other services
        }

        public IEnumerable<CommandPacket> GetAllCommands()
        {
            // Log the retrieval of commands
            _logger.LogInformation($"Retrieving all commands. Current queue count: {_commandQueue.Count}");
            return _commandQueue.ToArray(); // Return a copy of the queue
        }
    }
}
