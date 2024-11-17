using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CDH_GroundStation_Group6;

namespace CDH_GroundStation_Tests
{
    [TestClass]
    public class StatusTests
    {
        private Status status;

        [TestInitialize]
        public void Setup()
        {
            status = new Status();
        }

        [TestMethod]
        public void TestDefaultStatusState_IsOffline()
        {
            // Arrange & Act are done in Setup

            // Assert
            Assert.AreEqual("OFFLINE", status.StatusState, "Default StatusState should be 'OFFLINE'");
        }

        [TestMethod]
        public void TestStatusState_SetOnline()
        {
            // Arrange
            string expectedState = "ONLINE";

            // Act
            status.StatusState = expectedState;

            // Assert
            Assert.AreEqual(expectedState, status.StatusState, "StatusState should be set to 'ONLINE'");
        }

        [TestMethod]
        public void TestStatusState_SetEmptyString()
        {
            // Arrange
            string expectedState = "";

            // Act
            status.StatusState = expectedState;

            // Assert
            Assert.AreEqual(expectedState, status.StatusState, "StatusState should allow an empty string");
        }

        [TestMethod]
        public void TestStatusState_SetNull()
        {
            // Arrange
            string expectedState = null;

            // Act
            status.StatusState = expectedState;

            // Assert
            Assert.IsNull(status.StatusState, "StatusState should allow null values");
        }

        [TestMethod]
        public void TestStatusState_SetArbitraryValue()
        {
            // Arrange
            string arbitraryState = "MAINTENANCE";

            // Act
            status.StatusState = arbitraryState;

            // Assert
            Assert.AreEqual(arbitraryState, status.StatusState, "StatusState should accept arbitrary string values");
        }

        [TestMethod]
        public void TestStatusState_ToggleOnlineOffline()
        {
            // Arrange & Act
            status.StatusState = "ONLINE";
            Assert.AreEqual("ONLINE", status.StatusState, "StatusState should be 'ONLINE' after setting it");

            status.StatusState = "OFFLINE";
            Assert.AreEqual("OFFLINE", status.StatusState, "StatusState should be 'OFFLINE' after setting it");
        }
        [TestMethod]
        public void ADV_StatusState_InvalidValue_Ignored()
        {
            // Arrange
            string invalidState = "INVALID_STATE";

            // Act
            status.StatusState = invalidState;

            // Assert
            Assert.AreEqual(invalidState, status.StatusState, "StatusState should accept any string, including invalid values, unless explicitly restricted.");
        }

        [TestMethod]
        public void ADV_StatusState_PreserveCaseSensitivity()
        {
            // Arrange
            string mixedCaseState = "OnLiNe";

            // Act
            status.StatusState = mixedCaseState;

            // Assert
            Assert.AreEqual(mixedCaseState, status.StatusState, "StatusState should preserve case sensitivity for values.");
        }

        [TestMethod]
        public void ADV_StatusState_LongString()
        {
            // Arrange
            string longStringState = new string('A', 1000); // 1000 characters

            // Act
            status.StatusState = longStringState;

            // Assert
            Assert.AreEqual(longStringState, status.StatusState, "StatusState should handle long string values without truncation or errors.");
        }

        [TestMethod]
        public void ADV_StatusState_WhitespaceOnly()
        {
            // Arrange
            string whitespaceState = "   "; // String with only whitespace

            // Act
            status.StatusState = whitespaceState;

            // Assert
            Assert.AreEqual(whitespaceState, status.StatusState, "StatusState should accept strings with only whitespace.");
        }

        [TestMethod]
        public void ADV_StatusState_SpecialCharacters()
        {
            // Arrange
            string specialCharactersState = "!@#$%^&*()_+";

            // Act
            status.StatusState = specialCharactersState;

            // Assert
            Assert.AreEqual(specialCharactersState, status.StatusState, "StatusState should accept strings containing special characters.");
        }

        [TestMethod]
        public void ADV_StatusState_ToggleMultipleStates()
        {
            // Arrange
            string[] states = { "ONLINE", "OFFLINE", "MAINTENANCE", "ERROR" };

            // Act & Assert
            foreach (var state in states)
            {
                status.StatusState = state;
                Assert.AreEqual(state, status.StatusState, $"StatusState should toggle to '{state}' correctly.");
            }
        }

    }
}
