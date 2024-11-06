using ground_station.Models;
using ground_station.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using Castle.Core.Logging;
using ground_station.Models; // Ensure this is the correct namespace for CommandPacket
using ground_station.Services;

namespace GroundStation.Tests
{
    public class UplinkCommunicationServiceTests
    {
        private readonly Mock<ILogger<UplinkCommunicationService>> _mockLogger;
        private readonly Mock<LoggingAndMonitoringService> _mockLoggingService;
        private readonly UplinkCommunicationService _service;

        public UplinkCommunicationServiceTests()
        {
            _mockLogger = new Mock<ILogger<UplinkCommunicationService>>();
            var mockLoggingServiceLogger = new Mock<ILogger<LoggingAndMonitoringService>>();
            _mockLoggingService = new Mock<LoggingAndMonitoringService>(mockLoggingServiceLogger.Object);
            _service = new UplinkCommunicationService(_mockLoggingService.Object, _mockLogger.Object);
        }

        [Fact]
        public void SendCommand_ShouldLogInformation_WhenCommandSent()
        {
            // Arrange
            var commandPacket = new CommandPacket(
                command: "CollectData",
                parameters: new Dictionary<string, string>
                {
                { "dataId", "12345" },
                { "dataType", "scientific" }
                },
                source: "Scientific Operations"
            );

            // Act
            _service.SendCommand(commandPacket);

            // Assert
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Sending command")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }

        [Fact]
        public void SendCommand_ShouldLogWarning_WhenCommandIsEmpty()
        {
            // Arrange
            var commandPacket = new CommandPacket(
                command: "",
                parameters: new Dictionary<string, string>(),
                source: "Ground Station"
            );

            // Act
            _service.SendCommand(commandPacket);

            // Assert
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Command is null or empty")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }

        [Fact]
        public void SendCommand_ShouldLogError_WhenParametersAreMissing()
        {
            // Arrange
            var commandPacket = new CommandPacket(
                command: "CollectData",
                parameters: null,
                source: "Scientific Operations",
                destination: "Spacecraft"
            );

            // Act
            _service.SendCommand(commandPacket);

            // Assert
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error: Parameters cannot be null or empty.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once
            );
        }

        [Fact]
        public void SendCommand_ShouldQueueCommand_WhenValidCommandIsSent()
        {
            // Arrange
            var commandPacket = new CommandPacket(
                command: "TransmitData",
                parameters: new Dictionary<string, string> { { "fileId", "7890" } },
                source: "Ground Station",
                destination: "PayloadOps"
            );

            // Act
            _service.SendCommand(commandPacket);

            // Assert that the command was enqueued
            var queuedCommands = _service.GetAllCommands();
            Assert.Single(queuedCommands);
            Assert.Contains(commandPacket, queuedCommands);
        }

        [Fact]
        public void SendCommand_ShouldLogInformation_WhenCommandIsQueued()
        {
            // Arrange
            var commandPacket = new CommandPacket(
                command: "TransmitData",
                parameters: new Dictionary<string, string> { { "fileId", "7890" } },
                source: "Ground Station",
                destination: "PayloadOps"
            );

            // Act
            _service.SendCommand(commandPacket);

            // Assert
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Command TransmitData added to the queue for processing")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }

        [Fact]
        public void GetAllCommands_ShouldReturnAllQueuedCommands()
        {
            // Arrange
            var commandPacket1 = new CommandPacket(
                command: "Command1",
                parameters: new Dictionary<string, string> { { "param1", "value1" } },
                source: "Source1",
                destination: "Destination1"
            );

            var commandPacket2 = new CommandPacket(
                command: "Command2",
                parameters: new Dictionary<string, string> { { "param2", "value2" } },
                source: "Source2",
                destination: "Destination2"
            );

            // Act
            _service.SendCommand(commandPacket1);
            _service.SendCommand(commandPacket2);
            var allCommands = _service.GetAllCommands();

            // Assert
            Assert.Equal(2, allCommands.Count());
            Assert.Contains(commandPacket1, allCommands);
            Assert.Contains(commandPacket2, allCommands);
        }

        [Fact]
        public void SendCommand_ShouldLogError_WhenCommandPacketIsNull()
        {
            // Act
            _service.SendCommand(null);

            // Assert
            _mockLoggingService.Verify(
                logger => logger.LogInfo(It.Is<string>(s => s.Contains("Command packet is null."))),
                Times.Once
            );
        }
    }

        public class DownlinkCommunicationServiceTests
    {
        private readonly Mock<ILogger<DownlinkCommunicationService>> _mockLogger;
        private readonly DownlinkCommunicationService _service;

        public DownlinkCommunicationServiceTests()
        {
            _mockLogger = new Mock<ILogger<DownlinkCommunicationService>>();
            _service = new DownlinkCommunicationService(_mockLogger.Object);
        }

        [Fact]
        public void ReceiveCommand_ShouldLogWarning_WhenCommandPacketIsBad()
        {
            // Arrange: Create a bad CommandPacket (use an empty command string or invalid parameters)
            var commandPacket = new CommandPacket(command: "", parameters: new Dictionary<string, string>(), source: "Spacecraft");

            // Act: Call ReceiveCommand with the bad packet
            _service.ReceiveCommand(commandPacket);

            // Assert: Verify that a warning is logged
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Received a bad command packet")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }


        [Fact]
        public void ReceiveCommand_ShouldEnqueueValidPacket_WhenCommandIsValid()
        {
            // Arrange: Create a valid CommandPacket
            var commandPacket = new CommandPacket(command: "StartAnalysis", parameters: new Dictionary<string, string> { { "dataId", "12345" } }, source: "Spacecraft");

            // Act: Call ReceiveCommand with the valid packet
            _service.ReceiveCommand(commandPacket);

            // Assert: Verify that the command was enqueued
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Command enqueued successfully")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }

        [Fact]
        public void GetNextReceivedCommand_ShouldReturnNextCommand_WhenQueueIsNotEmpty()
        {
            // Arrange: Create a valid CommandPacket and ensure it is received and added to the queue
            var commandPacket = new CommandPacket(
                command: "CollectData",
                parameters: new Dictionary<string, string> { { "dataId", "12345" }, { "dataType", "scientific" } },
                source: "Spacecraft",
                destination: "Ground Station"
            );

            _service.ReceiveCommand(commandPacket); // Simulate receiving the command

            // Act: Try to get the next received command from the queue
            var result = _service.GetNextReceivedCommand();

            // Assert: Ensure that the result is not null and the correct command is returned
            Assert.NotNull(result);  // The command should not be null
            Assert.Equal(commandPacket.Command, result?.Command);  // Ensure the returned command matches the original
            Assert.Equal(commandPacket.Parameters, result?.Parameters);  // Ensure parameters match
        }


        [Fact]
        public void GetNextReceivedCommand_ShouldReturnNull_WhenQueueIsEmpty()
        {
            // Act: Try to get a command when the queue is empty
            var result = _service.GetNextReceivedCommand();

            // Assert: Verify that null is returned
            Assert.Null(result);

            // Verify that a warning was logged
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("No received commands available")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }

        [Fact]
        public void SendBadPacketToUplink_ShouldLogInformation_WhenBadPacketIsSent()
        {
            // Arrange: Create a bad CommandPacket (e.g., empty command)
            var badCommandPacket = new CommandPacket(
                command: string.Empty,  // Empty command to simulate a bad packet
                parameters: new Dictionary<string, string>(), // Empty parameters
                source: "Spacecraft",
                destination: "Ground Station"
            );

            // Act: Call the method that processes the bad packet
            _service.ReceiveCommand(badCommandPacket); // This should trigger the bad packet logic

            // Assert: Verify that the logger's Log method was called with the expected message
            _mockLogger.Verify(log => log.Log(It.Is<LogLevel>(level => level == LogLevel.Information),
                                              It.Is<EventId>(eventId => eventId.Id == 0),
                                              It.Is<It.IsAnyType>((obj, type) => obj.ToString().Contains("Sending bad command packet back to uplink")),
                                              It.IsAny<Exception>(),
                                              It.Is<Func<It.IsAnyType, Exception, string>>((v, e) => true)),
                              Times.Once);
        }


    }

    public class TestLogger<T> : ILogger<T>
    {
        public List<string> LogMessages { get; } = new List<string>();

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                LogMessages.Add(formatter(state, exception));
            }
        }
    }

    public class DataVerificationServiceTests
    {
        private readonly TestLogger<DataVerificationService> _testLogger;
        private readonly DataVerificationService _service;

        public DataVerificationServiceTests()
        {
            _testLogger = new TestLogger<DataVerificationService>();
            _service = new DataVerificationService(_testLogger);
        }

        [Fact]
        public void VerifyCommandPacket_NullPacket_ReturnsFalse_LogsWarning()
        {
            // Arrange
            CommandPacket packet = null;

            // Act
            var result = _service.VerifyCommandPacket(packet);

            // Assert
            Assert.False(result);
            Assert.Contains("Command packet is null.", _testLogger.LogMessages);
        }

        [Fact]
        public void VerifyCommandPacket_EmptyCommand_ReturnsFalse_LogsWarning()
        {
            // Arrange
            var packet = new CommandPacket("", new Dictionary<string, string>());

            // Act
            var result = _service.VerifyCommandPacket(packet);

            // Assert
            Assert.False(result);
            Assert.Contains("Command is null or empty.", _testLogger.LogMessages);
        }

        [Fact]
        public void VerifyParameters_NullParameters_ReturnsFalse_LogsWarning()
        {
            // Arrange
            Dictionary<string, string> parameters = null;

            // Act
            var result = _service.VerifyParameters(parameters);

            // Assert
            Assert.False(result);
            Assert.Contains("Parameters dictionary is null or empty.", _testLogger.LogMessages);
        }

        [Fact]
        public void VerifyParameters_EmptyParameters_ReturnsFalse_LogsWarning()
        {
            // Arrange
            var parameters = new Dictionary<string, string>();

            // Act
            var result = _service.VerifyParameters(parameters);

            // Assert
            Assert.False(result);
            Assert.Contains("Parameters dictionary is null or empty.", _testLogger.LogMessages);
        }

        [Fact]
        public void VerifyParameters_InvalidKey_ReturnsFalse_LogsWarning()
        {
            // Arrange
            var parameters = new Dictionary<string, string>
            {
                { "", "validValue" }
            };

            // Act
            var result = _service.VerifyParameters(parameters);

            // Assert
            Assert.False(result);
            Assert.Contains("Invalid parameter: Key='', Value='validValue'", _testLogger.LogMessages);
        }

        [Fact]
        public void VerifyParameters_InvalidValue_ReturnsFalse_LogsWarning()
        {
            // Arrange
            var parameters = new Dictionary<string, string>
            {
                { "validKey", "" }
            };

            // Act
            var result = _service.VerifyParameters(parameters);

            // Assert
            Assert.False(result);
            Assert.Contains("Invalid parameter: Key='validKey', Value=''", _testLogger.LogMessages);
        }

        [Fact]
        public void VerifyCommandPacket_ValidPacket_ReturnsTrue_LogsInformation()
        {
            // Arrange
            var packet = new CommandPacket(
                "TestCommand",
                new Dictionary<string, string>
                {
                    { "param1", "value1" },
                    { "param2", "value2" }
                });

            // Act
            var result = _service.VerifyCommandPacket(packet);

            // Assert
            Assert.True(result);
            Assert.Contains("Command packet TestCommand verified successfully.", _testLogger.LogMessages);
        }
    }
}