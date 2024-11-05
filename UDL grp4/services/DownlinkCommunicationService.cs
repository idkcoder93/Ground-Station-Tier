using Microsoft.Extensions.Logging;
using ground_station.Models;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace ground_station.Services
{
    public class DownlinkCommunicationService
    {
        private readonly ILogger<DownlinkCommunicationService> _logger;
        private readonly ConcurrentQueue<CommandPacket> _receivedQueue;

        public DownlinkCommunicationService(ILogger<DownlinkCommunicationService> logger)
        {
            _logger = logger;
            _receivedQueue = new ConcurrentQueue<CommandPacket>();
        }

        public void ReceiveCommand(CommandPacket commandPacket)
        {
            // Call the VerifyAndProcessCommand method
            VerifyAndProcessCommand(commandPacket);
        }

        public void VerifyAndProcessCommand(CommandPacket commandPacket)
        {
            // Check for command validity
            bool isBadPacket = string.IsNullOrWhiteSpace(commandPacket.Command) || commandPacket.Parameters == null || !commandPacket.Parameters.Any();

            if (isBadPacket)
            {
                // Log the bad packet and send it back
                _logger.LogWarning($"Received a bad command packet: {commandPacket.Command} with parameters: {JsonConvert.SerializeObject(commandPacket.Parameters)}");
                SendBadPacketToUplink(commandPacket); // Implement this method to send back to uplink
            }
            else
            {
                // Enqueue the valid command packet for further processing
                _receivedQueue.Enqueue(commandPacket);
                _logger.LogInformation("Command enqueued successfully.");
            }
        }

        public CommandPacket? GetNextReceivedCommand()
        {
            if (_receivedQueue.TryDequeue(out var commandPacket))
            {
                _logger.LogInformation($"Processing received command: {commandPacket.Command} with parameters: {commandPacket.Parameters}");
                return commandPacket;
            }

            _logger.LogWarning("No received commands available to process.");
            return null;
        }

        public IEnumerable<CommandPacket> GetAllReceivedCommands()
        {
            return _receivedQueue.ToArray();
        }

        public void SendBadPacketToUplink(CommandPacket commandPacket)
        {
            // Implementation to send the bad packet back to the uplink
            // For example, you could use a method to log or transmit the bad packet.
            _logger.LogInformation($"Sending bad command packet back to uplink: {commandPacket.Command}");
            // Here you would implement the actual sending logic.
        }
    }
}
