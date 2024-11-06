using ground_station.Models;
using ground_station.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;

namespace GroundStation.Tests
{
    public class UplinkCommunicationServiceTests
    {
        private readonly Mock<ILogger<UplinkCommunicationService>> _mockLogger;
        private readonly UplinkCommunicationService _service;

        public UplinkCommunicationServiceTests()
        {
            // Initialize the mock logger
            _mockLogger = new Mock<ILogger<UplinkCommunicationService>>();

            // Initialize the UplinkCommunicationService with the mock logger
            _service = new UplinkCommunicationService(_mockLogger.Object);
        }

        [Fact]
        public void SendCommand_ShouldLogInformation_WhenCommandSent()
        {
            // Arrange: Create a valid CommandPacket object
            var commandPacket = new CommandPacket(
                command: "CollectData", // Command is now properly initialized
                parameters: new Dictionary<string, string>
                {
                    { "dataId", "12345" },
                    { "dataType", "scientific" },
                    { "location", "Mars" }
                },
                source: "Scientific Operations"
            );

            // Act: Call the SendCommand method
            _service.SendCommand(commandPacket);

            // Assert: Verify that the logger was called with the expected information
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Data sent from {commandPacket.Source}")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );

            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Sending command: {commandPacket.Command}")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }

        [Fact]
        public void SendCommand_ShouldLogError_WhenCommandPacketIsNull()
        {
            // Arrange: Create a null CommandPacket object
            CommandPacket commandPacket = null;

            // Act: Call the SendCommand method with the null packet
            _service.SendCommand(commandPacket);

            // Assert: Verify that an error message is logged for the null packet
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Command packet is null.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }

        [Fact]
        public void SendCommand_ShouldLogError_WhenCommandIsNullOrEmpty()
        {
            // Arrange: Create a CommandPacket object with an empty command
            var commandPacket = new CommandPacket(
                command: "", // Empty Command, this will simulate an invalid packet
                parameters: new Dictionary<string, string>
                {
                    { "dataId", "12345" },
                    { "dataType", "scientific" },
                    { "location", "Mars" }
                },
                source: "Scientific Operations"
            );

            // Act: Call the SendCommand method with the invalid command
            _service.SendCommand(commandPacket);

            // Assert: Verify that an error message is logged for the empty command
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Command is null or empty.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }

        [Fact]
        public void SendCommand_ShouldLogInformation_WhenParametersAreNull()
        {
            // Arrange: Create a CommandPacket object with null parameters
            var commandPacket = new CommandPacket(
                command: "CollectData",
                parameters: null, // Null Parameters
                source: "Spacecraft" // Source
            );

            // Act: Call the SendCommand method with the invalid parameters
            _service.SendCommand(commandPacket);

            // Assert: Verify that an information message is logged for the null parameters
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,  // Correct LogLevel
                    It.IsAny<EventId>(),    // Allow any EventId
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Parameters are null for command: CollectData")),  // Check that the message contains the expected text
                    It.IsAny<Exception>(),  // Allow any exception
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),  // Allow any formatter
                Times.Once, // Verify it's called exactly once
                "Expected log message was not found for null parameters."
            );
        }

    }
}
