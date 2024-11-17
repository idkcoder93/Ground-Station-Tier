using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dashboard;
using MongoDB.Driver;

namespace Dashboard_Tests
{
    [TestClass]
    public class CommandTests
    {
        private Command command;

        [TestInitialize]
        public void Setup()
        {
            command = new Command();
        }

        [TestMethod]
        public void TestDefaultCommandType_IsNull()
        {
            // Arrange & Act are done in Setup

            // Assert
            Assert.IsNull(command.CommandType, "Default CommandType should be null.");
        }

        [TestMethod]
        public void TestSetCommandType()
        {
            // Arrange
            string commandType = "NAVIGATION";

            // Act
            command.CommandType = commandType;

            // Assert
            Assert.AreEqual(commandType, command.CommandType, "CommandType should be set to 'NAVIGATION'.");
        }

        [TestMethod]
        public void TestSetCommandType_EmptyString()
        {
            // Arrange
            string commandType = "";

            // Act
            command.CommandType = commandType;

            // Assert
            Assert.AreEqual(commandType, command.CommandType, "CommandType should allow an empty string.");
        }

        [TestMethod]
        public void TestDefaultSpeed_IsZero()
        {
            // Assert
            Assert.AreEqual(0.0, command.Speed, "Default Speed should be 0.");
        }

        [TestMethod]
        public void TestSetSpeed()
        {
            // Arrange
            double speed = 250.5;

            // Act
            command.Speed = speed;

            // Assert
            Assert.AreEqual(speed, command.Speed, "Speed should be set to 250.5.");
        }

        [TestMethod]
        public void TestSetNegativeSpeed()
        {
            // Arrange
            double speed = -50.0;

            // Act
            command.Speed = speed;

            // Assert
            Assert.AreEqual(speed, command.Speed, "Speed should accept negative values like -50.0.");
        }

        [TestMethod]
        public void TestDefaultLatitude_IsZero()
        {
            // Assert
            Assert.AreEqual(0.0, command.Latitude, "Default Latitude should be 0.");
        }

        [TestMethod]
        public void TestSetLatitude()
        {
            // Arrange
            double latitude = 45.0;

            // Act
            command.Latitude = latitude;

            // Assert
            Assert.AreEqual(latitude, command.Latitude, "Latitude should be set to 45.0.");
        }

        [TestMethod]
        public void TestSetLatitude_BoundaryValues()
        {
            // Arrange
            double minLatitude = -90.0;
            double maxLatitude = 90.0;

            // Act
            command.Latitude = minLatitude;
            Assert.AreEqual(minLatitude, command.Latitude, "Latitude should accept minimum boundary value -90.");

            command.Latitude = maxLatitude;
            Assert.AreEqual(maxLatitude, command.Latitude, "Latitude should accept maximum boundary value 90.");
        }

        [TestMethod]
        public void TestDefaultLongitude_IsZero()
        {
            // Assert
            Assert.AreEqual(0.0, command.Longitude, "Default Longitude should be 0.");
        }

        [TestMethod]
        public void TestSetLongitude()
        {
            // Arrange
            double longitude = 120.0;

            // Act
            command.Longitude = longitude;

            // Assert
            Assert.AreEqual(longitude, command.Longitude, "Longitude should be set to 120.0.");
        }

        [TestMethod]
        public void TestSetLongitude_BoundaryValues()
        {
            // Arrange
            double minLongitude = -180.0;
            double maxLongitude = 180.0;

            // Act
            command.Longitude = minLongitude;
            Assert.AreEqual(minLongitude, command.Longitude, "Longitude should accept minimum boundary value -180.");

            command.Longitude = maxLongitude;
            Assert.AreEqual(maxLongitude, command.Longitude, "Longitude should accept maximum boundary value 180.");
        }

        [TestMethod]
        public void TestDefaultAltitude_IsZero()
        {
            // Assert
            Assert.AreEqual(0.0, command.Altitude, "Default Altitude should be 0.");
        }

        [TestMethod]
        public void TestSetAltitude()
        {
            // Arrange
            double altitude = 35000.0;

            // Act
            command.Altitude = altitude;

            // Assert
            Assert.AreEqual(altitude, command.Altitude, "Altitude should be set to 35000.");
        }

        [TestMethod]
        public void TestSetNegativeAltitude()
        {
            // Arrange
            double altitude = -100.0;

            // Act
            command.Altitude = altitude;

            // Assert
            Assert.AreEqual(altitude, command.Altitude, "Altitude should accept negative values like -100.");
        }
        [TestMethod]
        public void TestSetAllPropertiesSimultaneously()
        {
            // Arrange
            string commandType = "LANDING";
            double speed = 500.5;
            double latitude = 25.0;
            double longitude = 135.0;
            double altitude = 10000.0;

            // Act
            command.CommandType = commandType;
            command.Speed = speed;
            command.Latitude = latitude;
            command.Longitude = longitude;
            command.Altitude = altitude;

            // Assert
            Assert.AreEqual(commandType, command.CommandType, "CommandType should be set to 'LANDING'.");
            Assert.AreEqual(speed, command.Speed, "Speed should be set to 500.5.");
            Assert.AreEqual(latitude, command.Latitude, "Latitude should be set to 25.0.");
            Assert.AreEqual(longitude, command.Longitude, "Longitude should be set to 135.0.");
            Assert.AreEqual(altitude, command.Altitude, "Altitude should be set to 10000.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        // Advanced test cases-----
        public void ADV_TestSetLatitude_OutOfBounds_ThrowsException()
        {
            // Arrange
            double invalidLatitude = 95.0; // Latitude should be between -90 and 90

            // Act
            command.Latitude = invalidLatitude; // This should throw an exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ADV_TestSetLongitude_OutOfBounds_ThrowsException()
        {
            // Arrange
            double invalidLongitude = 200.0; // Longitude should be between -180 and 180

            // Act
            command.Longitude = invalidLongitude; // This should throw an exception
        }

        [TestMethod]
        public void ADV_TestSetAltitude_MaximumValue()
        {
            // Arrange
            double maxAltitude = double.MaxValue; // Set to the largest possible double value

            // Act
            command.Altitude = maxAltitude;

            // Assert
            Assert.AreEqual(maxAltitude, command.Altitude, "Altitude should accept the maximum possible value.");
        }

    }
}
