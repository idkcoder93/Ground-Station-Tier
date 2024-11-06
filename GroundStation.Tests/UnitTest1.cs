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
        // Mock logger used for capturing log messages during tests
        private readonly Mock<ILogger<UplinkCommunicationService>> _mockLogger;

        // Mock logging service used for additional logging functionality
        private readonly Mock<LoggingAndMonitoringService> _mockLoggingService;

        // Instance of the service being tested
        private readonly UplinkCommunicationService _service;

        // Constructor to initialize the mock logger, mock logging service, and service instance
        public UplinkCommunicationServiceTests()
        {
            _mockLogger = new Mock<ILogger<UplinkCommunicationService>>();
            var mockLoggingServiceLogger = new Mock<ILogger<LoggingAndMonitoringService>>();
            _mockLoggingService = new Mock<LoggingAndMonitoringService>(mockLoggingServiceLogger.Object);
            _service = new UplinkCommunicationService(_mockLoggingService.Object, _mockLogger.Object);
        }

        // Test case to verify logging behavior when a command is sent
        [Fact]
        public void SendCommand_ShouldLogInformation_WhenCommandSent()
        {
            // Arrange: Create a valid CommandPacket
            var commandPacket = new CommandPacket(
                command: "CollectData",
                parameters: new Dictionary<string, string>
                {
                { "dataId", "12345" },
                { "dataType", "scientific" }
                },
                source: "Scientific Operations"
            );

            // Act: Call SendCommand with the command packet
            _service.SendCommand(commandPacket);

            // Assert: Verify that information is logged
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

        // Test case to verify logging behavior when an empty command is sent
        [Fact]
        public void SendCommand_ShouldLogWarning_WhenCommandIsEmpty()
        {
            // Arrange: Create a CommandPacket with an empty command
            var commandPacket = new CommandPacket(
                command: "",
                parameters: new Dictionary<string, string>(),
                source: "Ground Station"
            );

            // Act: Call SendCommand with the empty command packet
            _service.SendCommand(commandPacket);

            // Assert: Verify that a warning is logged
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

        // Test case to verify logging behavior when parameters are missing
        [Fact]
        public void SendCommand_ShouldLogError_WhenParametersAreMissing()
        {
            // Arrange: Create a CommandPacket with null parameters
            var commandPacket = new CommandPacket(
                command: "CollectData",
                parameters: null,
                source: "Scientific Operations",
                destination: "Spacecraft"
            );

            // Act: Call SendCommand with the command packet
            _service.SendCommand(commandPacket);

            // Assert: Verify that an error is logged
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

        // Test case to verify that a valid command is queued correctly
        [Fact]
        public void SendCommand_ShouldQueueCommand_WhenValidCommandIsSent()
        {
            // Arrange: Create a valid CommandPacket
            var commandPacket = new CommandPacket(
                command: "TransmitData",
                parameters: new Dictionary<string, string> { { "fileId", "7890" } },
                source: "Ground Station",
                destination: "PayloadOps"
            );

            // Act: Call SendCommand with the valid command packet
            _service.SendCommand(commandPacket);

            // Assert: Verify that the command was enqueued
            var queuedCommands = _service.GetAllCommands();
            Assert.Single(queuedCommands); // Ensure only one command is queued
            Assert.Contains(commandPacket, queuedCommands); // Ensure the queued command matches the original
        }

        // Test case to verify logging behavior when a command is queued
        [Fact]
        public void SendCommand_ShouldLogInformation_WhenCommandIsQueued()
        {
            // Arrange: Create a valid CommandPacket
            var commandPacket = new CommandPacket(
                command: "TransmitData",
                parameters: new Dictionary<string, string> { { "fileId", "7890" } },
                source: "Ground Station",
                destination: "PayloadOps"
            );

            // Act: Call SendCommand with the valid command packet
            _service.SendCommand(commandPacket);

            // Assert: Verify that information is logged about the command being queued
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

        // Test case to verify that all queued commands are returned correctly
        [Fact]
        public void GetAllCommands_ShouldReturnAllQueuedCommands()
        {
            // Arrange: Create two valid CommandPackets
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

            // Act: Send both command packets
            _service.SendCommand(commandPacket1);
            _service.SendCommand(commandPacket2);
            var allCommands = _service.GetAllCommands();

            // Assert: Verify that both commands are returned
            Assert.Equal(2, allCommands.Count()); // Ensure two commands are returned
            Assert.Contains(commandPacket1, allCommands); // Ensure the first command is present
            Assert.Contains(commandPacket2, allCommands); // Ensure the second command is present
        }

        // Test case to verify logging behavior when a null command packet is sent
        [Fact]
        public void SendCommand_ShouldLogError_WhenCommandPacketIsNull()
        {
            // Act: Call SendCommand with a null command packet
            _service.SendCommand(null);

            // Assert: Verify that an error is logged for the null command packet
            _mockLoggingService.Verify(
                logger => logger.LogInfo(It.Is<string>(s => s.Contains("Command packet is null."))),
                Times.Once
            );
        }
    }

    public class DownlinkCommunicationServiceTests
    {
        // Mock logger used for capturing log messages during tests
        private readonly Mock<ILogger<DownlinkCommunicationService>> _mockLogger;

        // Instance of the service being tested
        private readonly DownlinkCommunicationService _service;

        // Constructor to initialize the mock logger and service
        public DownlinkCommunicationServiceTests()
        {
            _mockLogger = new Mock<ILogger<DownlinkCommunicationService>>();
            _service = new DownlinkCommunicationService(_mockLogger.Object);
        }

        // Test case to verify logging behavior when a bad command packet is received
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

        // Test case to verify that a valid command packet is enqueued
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

        // Test case to verify that the next command is returned when the queue is not empty
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

        // Test case to verify that null is returned when the queue is empty
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

        // Test case to verify logging behavior when a bad packet is sent to the uplink
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
        // Logger used for capturing log messages during tests
        private readonly TestLogger<DataVerificationService> _testLogger;

        // Instance of the service being tested
        private readonly DataVerificationService _service;

        // Constructor to initialize the logger and service
        public DataVerificationServiceTests()
        {
            _testLogger = new TestLogger<DataVerificationService>();
            _service = new DataVerificationService(_testLogger);
        }

        // Test case to verify behavior when a null command packet is provided
        [Fact]
        public void VerifyCommandPacket_NullPacket_ReturnsFalse_LogsWarning()
        {
            // Arrange: Set up a null command packet
            CommandPacket packet = null;

            // Act: Call the method under test
            var result = _service.VerifyCommandPacket(packet);

            // Assert: Verify the expected outcome
            Assert.False(result); // Should return false
            Assert.Contains("Command packet is null.", _testLogger.LogMessages); // Should log a warning
        }

        // Test case to verify behavior when an empty command is provided
        [Fact]
        public void VerifyCommandPacket_EmptyCommand_ReturnsFalse_LogsWarning()
        {
            // Arrange: Set up an empty command packet
            var packet = new CommandPacket("", new Dictionary<string, string>());

            // Act: Call the method under test
            var result = _service.VerifyCommandPacket(packet);

            // Assert: Verify the expected outcome
            Assert.False(result); // Should return false
            Assert.Contains("Command is null or empty.", _testLogger.LogMessages); // Should log a warning
        }

        // Test case to verify behavior when null parameters are provided
        [Fact]
        public void VerifyParameters_NullParameters_ReturnsFalse_LogsWarning()
        {
            // Arrange: Set up a null parameters dictionary
            Dictionary<string, string> parameters = null;

            // Act: Call the method under test
            var result = _service.VerifyParameters(parameters);

            // Assert: Verify the expected outcome
            Assert.False(result); // Should return false
            Assert.Contains("Parameters dictionary is null or empty.", _testLogger.LogMessages); // Should log a warning
        }

        // Test case to verify behavior when an empty parameters dictionary is provided
        [Fact]
        public void VerifyParameters_EmptyParameters_ReturnsFalse_LogsWarning()
        {
            // Arrange: Set up an empty parameters dictionary
            var parameters = new Dictionary<string, string>();

            // Act: Call the method under test
            var result = _service.VerifyParameters(parameters);

            // Assert: Verify the expected outcome
            Assert.False(result); // Should return false
            Assert.Contains("Parameters dictionary is null or empty.", _testLogger.LogMessages); // Should log a warning
        }

        // Test case to verify behavior when an invalid key is provided in parameters
        [Fact]
        public void VerifyParameters_InvalidKey_ReturnsFalse_LogsWarning()
        {
            // Arrange: Set up parameters with an invalid key (empty string)
            var parameters = new Dictionary<string, string>
        {
            { "", "validValue" } // Invalid key
        };

            // Act: Call the method under test
            var result = _service.VerifyParameters(parameters);

            // Assert: Verify the expected outcome
            Assert.False(result); // Should return false
            Assert.Contains("Invalid parameter: Key='', Value='validValue'", _testLogger.LogMessages); // Should log a warning
        }

        // Test case to verify behavior when an invalid value is provided in parameters
        [Fact]
        public void VerifyParameters_InvalidValue_ReturnsFalse_LogsWarning()
        {
            // Arrange: Set up parameters with an invalid value (empty string)
            var parameters = new Dictionary<string, string>
        {
            { "validKey", "" } // Invalid value
        };

            // Act: Call the method under test
            var result = _service.VerifyParameters(parameters);

            // Assert: Verify the expected outcome
            Assert.False(result); // Should return false
            Assert.Contains("Invalid parameter: Key='validKey', Value=''", _testLogger.LogMessages); // Should log a warning
        }

        // Test case to verify behavior when a valid command packet is provided
        [Fact]
        public void VerifyCommandPacket_ValidPacket_ReturnsTrue_LogsInformation()
        {
            // Arrange: Set up a valid command packet
            var packet = new CommandPacket(
                "TestCommand",
                new Dictionary<string, string>
                {
                { "param1", "value1" },
                { "param2", "value2" }
                });

            // Act: Call the method under test
            var result = _service.VerifyCommandPacket(packet);

            // Assert: Verify the expected outcome ```csharp
            Assert.True(result); // Should return true
            Assert.Contains("Command packet TestCommand verified successfully.", _testLogger.LogMessages); // Should log information
        }
    }
}