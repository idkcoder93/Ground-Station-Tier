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
    }
}
