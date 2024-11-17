using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dashboard;

namespace DashboardTests
{
    [TestClass]
    public class DestinationTests
    {
        [TestMethod]
        public void TestDestinationInfo_SetAndGet()
        {
            // Arrange
            Destination destination = new Destination();
            string expectedValue = "Mars";

            // Act
            destination.DestinationInfo = expectedValue;
            string actualValue = destination.DestinationInfo;

            // Assert
            Assert.AreEqual(expectedValue, actualValue, "The DestinationInfo property did not return the expected value.");
        }

        [TestMethod]
        public void TestDestinationInfo_DefaultValue()
        {
            // Arrange
            Destination destination = new Destination();

            // Act
            string defaultValue = destination.DestinationInfo;

            // Assert
            Assert.IsNull(defaultValue, "The default value of DestinationInfo should be null.");
        }

        [TestMethod]
        public void TestDestinationInfo_EmptyString()
        {
            // Arrange
            Destination destination = new Destination();

            // Act
            destination.DestinationInfo = string.Empty;

            // Assert
            Assert.AreEqual(string.Empty, destination.DestinationInfo, "The DestinationInfo property should accept and return an empty string.");
        }

        [TestMethod]
        public void TestDestinationInfo_SpecialCharacters()
        {
            // Arrange
            Destination destination = new Destination();
            string specialCharacters = "@#$%^&*()_+|";

            // Act
            destination.DestinationInfo = specialCharacters;

            // Assert
            Assert.AreEqual(specialCharacters, destination.DestinationInfo, "The DestinationInfo property should correctly handle special characters.");
        }
    }
}
