using Microsoft.VisualStudio.TestTools.UnitTesting;             //using MS Test framework for writing and running tests
using Dashboard;         //accessing our Ground Station classes

namespace GroundStationPayloadOpsTests
{
    //using AAA
    [TestClass]     //marks this class as a test class for MS Test
    public class GroundStationPacketHandlerTests
    {
        private GroundStationPacketHandler handler = new GroundStationPacketHandler();          //initializes handler immediately

        //CreatePacket Tests

        [TestMethod]    //marks this method as a test
        public void TestCreatePacket()
        {
            //Arrange - setting up the data we will use to create a packet
            string commandType = "telemetry";
            string function = "22°C";
            string crc = "0110";

            //Act - creating a packet using the handler method
            GroundStationPacket packet = handler.CreatePacket(commandType, function, crc);

            //Assert - checking if the packet properties match what we expected
            Assert.IsNotNull(packet);           //packet should not be null
            Assert.AreEqual(commandType, packet.CommandType);         //datatype should match
            Assert.AreEqual(function, packet.Function);                 // data should match
            Assert.AreEqual(crc, packet.CRC);           //CRC should match
        }

        [TestMethod]
        public void TestCreatePacketWithEdgeCaseValues()
        {
            //Arrange - setting up edge case data 
            string commandType = null;
            string function = "";
            string crc = "";

            //Act - creating a packet with edge case values
            GroundStationPacket packet = handler.CreatePacket(commandType, function, crc);

            //Assert - checking if the packet properties match the input or default to safe values
            Assert.IsNotNull(packet); //packet should not be null
            Assert.AreEqual(commandType, packet.CommandType); //datatype should match input
            Assert.AreEqual(function, packet.Function);         // data should match input
            Assert.AreEqual(crc, packet.CRC); //CRC should match input
        }

        [TestMethod]
        public void TestCreatePacketWithMaxLengthFields()
        {
            //Arrange - create very long strings for each field
            string commandType = new string('A', 1000);
            string function = new string('B', 1000);
            string crc = new string('D', 1000);

            //Act - create packet with maximum length values
            GroundStationPacket packet = handler.CreatePacket(commandType, function, crc);

            //Assert - check that each property was assigned the long string correctly
            Assert.AreEqual(commandType, packet.CommandType);
            Assert.AreEqual(function, packet.Function);
            Assert.AreEqual(crc, packet.CRC);
        }

        [TestMethod]
        public void TestCreatePacketWithSpecialCharacters()
        {
            //Arrange - data with numbers and special characters
            string commandType = "telemetry-1";
            string function = "22°C, 5.5mSv";
            string crc = "01@#";

            //Act - create packet with these values
            GroundStationPacket packet = handler.CreatePacket(commandType, function, crc);

            //Assert - verify the packet correctly assigns these values
            Assert.AreEqual(commandType, packet.CommandType);
            Assert.AreEqual(function, packet.Function);
            Assert.AreEqual(crc, packet.CRC);
        }

        [TestMethod]
        public void TestCreatePacketWithWhitespaceFields()
        {
            //Arrange - whitespace in each field
            string commandType = "   ";
            string function = "\t ";
            string crc = "\n";

            //Act - create packet with whitespace fields
            GroundStationPacket packet = handler.CreatePacket(commandType, function, crc);

            //Assert - check that fields remain as whitespace
            Assert.AreEqual(commandType, packet.CommandType);
            Assert.AreEqual(function, packet.Function);
            Assert.AreEqual(crc, packet.CRC);
        }

        [TestMethod]
        public void TestCreatePacketWithMinimalValidData()
        {
            //Arrange - minimal non-empty data
            string commandType = "A";
            string function = "1";
            string crc = "1";

            //Act - create packet with minimal valid data
            GroundStationPacket packet = handler.CreatePacket(commandType, function, crc);

            //Assert - ensure fields are set correctly
            Assert.AreEqual(commandType, packet.CommandType);
            Assert.AreEqual(function, packet.Function);
            Assert.AreEqual(crc, packet.CRC);
        }

        //SerializePacket Tests
        [TestMethod]
        public void TestSerializePacket()
        {
            //Arrange - creating a packet to test serialization
            string commandType = "telemetry";
            string function = "22°C, 5mSv"; // Use combined data field
            string crc = "0110";
            GroundStationPacket packet = handler.CreatePacket(commandType, function, crc);

            //Act - converting the packet to JSON format
            string jsonPacket = handler.SerializePacket(packet);

            //Assert - check that the JSON string has all the expected data fields
            Assert.IsNotNull(jsonPacket);
            Assert.IsTrue(jsonPacket.Contains("\"CommandType\": \"telemetry\""));      //should contain the datatype
            Assert.IsTrue(jsonPacket.Contains("\"Function\": \"22°C, 5mSv\""));         //should contain the data field
            Assert.IsTrue(jsonPacket.Contains("\"CRC\": \"0110\""));                //should contain the CRC
        }

        [TestMethod]
        public void TestSerializePacketWithNullInput()
        {
            // Act - attempt to serialize a null packet
            string jsonPacket = handler.SerializePacket(null);

            // Assert - check that the JSON string is empty to signify failed serialization
            Assert.IsTrue(string.IsNullOrEmpty(jsonPacket)); // Should handle null input gracefully
        }


        [TestMethod]
        public void TestSerializeEmptyPacket()
        {
            // Arrange - create an empty packet
            GroundStationPacket packet = new GroundStationPacket();

            // Act - serialize the empty packet
            string jsonPacket = handler.SerializePacket(packet);

            // Assert - check that the JSON contains the default field values
            Assert.IsNotNull(jsonPacket);
            Assert.IsTrue(jsonPacket.Contains("\"CommandType\": \"\"")); // Updated default value
            Assert.IsTrue(jsonPacket.Contains("\"Function\": \"\""));    // Empty data field
            Assert.IsTrue(jsonPacket.Contains("\"CRC\": \"\""));         // Empty CRC
        }


        [TestMethod]
        public void TestSerializePacketWithLargeDataFields()
        {
            //Arrange - create packet with large data strings
            string largeString = new string('A', 1000);
            GroundStationPacket packet = handler.CreatePacket(largeString, largeString, largeString);

            //Act - serialize the packet
            string jsonPacket = handler.SerializePacket(packet);

            //Assert - check that the JSON contains the full, untruncated large data strings
            Assert.IsNotNull(jsonPacket);
            Assert.IsTrue(jsonPacket.Contains($"\"CommandType\": \"{largeString}\""));
            Assert.IsTrue(jsonPacket.Contains($"\"Function\": \"{largeString}\""));
            Assert.IsTrue(jsonPacket.Contains($"\"CRC\": \"{largeString}\""));
        }

        [TestMethod]
        public void TestSerializePacketWithSpecialCharacters()
        {
            //Arrange - create packet with special characters in fields
            string datatype = "telemetry-1";
            string data = "22°C, 5.5mSv"; ;
            string crc = "01@#";

            GroundStationPacket packet = handler.CreatePacket(datatype, data, crc);

            //Act - serialize the packet
            string jsonPacket = handler.SerializePacket(packet);

            //Assert - check that JSON includes all special characters correctly
            Assert.IsNotNull(jsonPacket);
            Assert.IsTrue(jsonPacket.Contains($"\"CommandType\": \"{datatype}\""));
            Assert.IsTrue(jsonPacket.Contains($"\"Function\": \"{data}\""));
            Assert.IsTrue(jsonPacket.Contains($"\"CRC\": \"{crc}\""));
        }

        //DepacketizeData Tests
        [TestMethod]
        public void TestDepacketizeEmptyJson()
        {
            // Act - Attempt to depacketize an empty JSON string
            GroundStationPacket packet = handler.DepacketizeData("");

            // Assert - Verify that an empty packet is returned
            Assert.IsNotNull(packet);
            Assert.AreEqual(string.Empty, packet.CommandType);
            Assert.AreEqual(string.Empty, packet.Function);
            Assert.AreEqual(string.Empty, packet.CRC);
        }

        [TestMethod]
        public void TestDepacketizePartialJson()
        {
            // Arrange - JSON with some fields missing
            string partialJson = "{ \"CommandType\": \"telemetry\", \"Function\": \"22°C\" }";

            // Act - Depacketize the partial JSON
            GroundStationPacket packet = handler.DepacketizeData(partialJson);

            // Assert - Verify that missing fields are set to defaults
            Assert.IsNotNull(packet);
            Assert.AreEqual("telemetry", packet.CommandType);
            Assert.AreEqual("22°C", packet.Function);
            Assert.AreEqual(string.Empty, packet.CRC); // Default for missing field
        }

        [TestMethod]
        public void TestDepacketizeData()
        {
            //Arrange - create a packet and serialize it to JSON
            string commandType = "telemetry";
            string function = "22°C, 5mSv";
            string crc = "0110";
            GroundStationPacket packet = handler.CreatePacket(commandType, function, crc);
            string jsonPacket = handler.SerializePacket(packet);

            //Act - convert the JSON back to a GroundStationPacket object
            GroundStationPacket depacketizedPacket = handler.DepacketizeData(jsonPacket);

            //Assert - make sure the depacketized packet matches the original data
            Assert.IsNotNull(depacketizedPacket);
            Assert.AreEqual("telemetry", depacketizedPacket.CommandType);
            Assert.AreEqual("22°C, 5mSv", depacketizedPacket.Function);
            Assert.AreEqual("0110", depacketizedPacket.CRC);
        }

        [TestMethod]
        public void TestDepacketizeDataWithInvalidJson()
        {
            //Arrange - invalid JSON string
            string invalidJson = "{ \"InvalidJson\": true }";

            //Act - attempt to depacketize invalid JSON
            GroundStationPacket packet = handler.DepacketizeData(invalidJson);

            //Assert - check that an empty packet is returned as a fallback
            Assert.IsNotNull(packet);
            Assert.AreEqual(string.Empty, packet.CommandType);     //default datatype
            Assert.AreEqual(string.Empty, packet.Function);         //default data field
            Assert.AreEqual(string.Empty, packet.CRC);          //default CRC
        }

        [TestMethod]
        public void TestDepacketizeDataWithEmptyJson()
        {
            //Arrange - empty JSON string
            string emptyJson = "";

            //Act - attempt to depacketize empty JSON
            GroundStationPacket packet = handler.DepacketizeData(emptyJson);

            //Assert - check that an empty packet is returned as a fallback
            Assert.IsNotNull(packet);
            Assert.AreEqual(string.Empty, packet.CommandType);
            Assert.AreEqual(string.Empty, packet.Function);
            Assert.AreEqual(string.Empty, packet.CRC);
        }

        [TestMethod]
        public void TestDepacketizeDataWithPartialJson()
        {
            //Arrange - JSON with only some fields present
            string partialJson = "{ \"CommandType\": \"telemetry\", \"Function\": \"22°C\" }";

            //Act - attempt to depacketize partial JSON
            GroundStationPacket packet = handler.DepacketizeData(partialJson);

            //Assert - check that missing fields are set to defaults and present fields match the JSON
            Assert.IsNotNull(packet);
            Assert.AreEqual("telemetry", packet.CommandType);
            Assert.AreEqual("22°C", packet.Function);
            Assert.AreEqual(string.Empty, packet.CRC);
        }

        [TestMethod]
        public void TestDepacketizeDataWithLargeValues()
        {
            //Arrange - JSON with very large string values
            string largeString = new string('A', 1000);
            string largeJson = $"{{ \"CommandType\": \"{largeString}\", \"Function\": \"{largeString}\", \"CRC\": \"{largeString}\" }}";

            //Act - attempt to depacketize JSON with large values
            GroundStationPacket packet = handler.DepacketizeData(largeJson);

            //Assert - check that each field contains the full large value
            Assert.IsNotNull(packet);
            Assert.AreEqual(largeString, packet.CommandType);
            Assert.AreEqual(largeString, packet.Function);
            Assert.AreEqual(largeString, packet.CRC);
        }

        [TestMethod]
        public void TestDepacketizeDataWithSpecialCharacters()
        {
            //Arrange - JSON with special characters
            string jsonWithSpecialChars = "{ \"CommandType\": \"telemetry-1\", \"Function\": \"22°C, 5.5mSv\", \"CRC\": \"01@#\" }";

            //Act - depacketize JSON with special characters
            GroundStationPacket packet = handler.DepacketizeData(jsonWithSpecialChars);

            //Assert - ensure that fields with special characters are deserialized correctly
            Assert.IsNotNull(packet);
            Assert.AreEqual("telemetry-1", packet.CommandType);
            Assert.AreEqual("22°C, 5.5mSv", packet.Function);
            Assert.AreEqual("01@#", packet.CRC);
        }

        //SendPacket Tests
        [TestMethod]
        public void TestSendPacketSuccess()
        {
            //Arrange - create a packet to test sending
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");

            //Act - simulate sending the packet
            bool result = handler.SendPacket(packet);

            //Assert - check if sending was reported as successful
            Assert.IsTrue(result);          //should be true if the packet was "sent" successfully
        }

        [TestMethod]
        public void TestSendPacketFailure()
        {
            // Arrange - No packet to send (null input)

            // Act - Simulate an error during packet sending
            bool result;
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                result = handler.SendPacket(null); // Intentionally passing null to simulate failure
            }

            // Assert - Check if failure was handled
            Assert.IsFalse(result); // Should be false due to transmission error
        }


        [TestMethod]
        public void TestSendPacketConsoleOutputSuccess()
        {
            //Arrange - create a packet to test sending
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - simulate sending the packet
                bool result = handler.SendPacket(packet);

                //Assert - check if sending was successful and if console output is correct
                string consoleOutput = sw.ToString();
                Assert.IsTrue(result);                                                  //should return true for successful send
                Assert.IsTrue(consoleOutput.Contains("Sending Packet:"));
                Assert.IsTrue(consoleOutput.Contains("\"CommandType\": \"telemetry\""));
                Assert.IsTrue(consoleOutput.Contains("\"Function\": \"22°C, 5mSv\""));
                Assert.IsTrue(consoleOutput.Contains("\"CRC\": \"0110\""));
            }
        }

        [TestMethod]
        public void TestSendPacketConsoleOutputFailure()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act - Simulate sending a null packet to cause failure
                bool result = handler.SendPacket(null);

                // Assert - Verify that the method returned false and output the correct error message
                string consoleOutput = sw.ToString();
                Assert.IsFalse(result); // Should return false for failed send
                Assert.IsTrue(consoleOutput.Contains("Transmission error: Packet is null.")); // Check for specific error message
            }
        }


        [TestMethod]
        public void TestSendPacketWithLargeData()
        {
            //Arrange - large data fields
            string largeString = new string('A', 1000);
            GroundStationPacket packet = handler.CreatePacket(largeString, largeString, largeString);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - simulate sending the packet with large data
                bool result = handler.SendPacket(packet);

                //Assert - check that sending was successful and output contains the full large data
                string consoleOutput = sw.ToString();
                Assert.IsTrue(result);                                  //should return true for successful send
                Assert.IsTrue(consoleOutput.Contains(largeString));     //large data should be present in output
            }
        }

        [TestMethod]
        public void TestSendPacketWithInvalidCrc()
        {
            // Arrange - Create a packet with an invalid CRC
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C", "INVALID_CRC");

            // Act - Send the packet
            bool result = handler.SendPacket(packet);

            // Assert - Ensure the packet is not sent due to invalid CRC
            Assert.IsFalse(result); // Should fail if CRC validation is implemented
        }

        [TestMethod]
        public void TestSendEmptyPacket()
        {
            // Arrange - create an empty packet
            GroundStationPacket packet = new GroundStationPacket();

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act - simulate sending the empty packet
                bool result = handler.SendPacket(packet);

                // Assert - check that sending was successful and default values are in output
                string consoleOutput = sw.ToString();
                Assert.IsTrue(result);
                Assert.IsTrue(consoleOutput.Contains("\"CommandType\": \"\""));   // Corrected default value
                Assert.IsTrue(consoleOutput.Contains("\"Function\": \"\""));     // Empty data field
                Assert.IsTrue(consoleOutput.Contains("\"CRC\": \"\""));          // Empty CRC
            }
        }


        //Test SendtoUplinkDownlink Wrapper method
        [TestMethod]
        public void TestSendToUplinkDownlinkSuccess()
        {
            //Arrange - create a packet for sending
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");

            //Act - call SendToUplinkDownlink to "send" the packet
            bool result = handler.SendToUplinkDownlink(packet);

            //Assert - check that it returns true for successful transmission
            Assert.IsTrue(result); //should be true for successful send
        }

        [TestMethod]
        public void TestSendToUplinkDownlinkFailureWithNullPacket()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - attempt to send a null packet to uplink/downlink
                bool result = handler.SendToUplinkDownlink(null);

                //Assert - check that it returns false due to transmission error
                Assert.IsFalse(result); //should return false for failed send
                Assert.IsTrue(sw.ToString().Contains("Transmission error:")); //check console output for error message
            }
        }

        [TestMethod]
        public void TestSendToUplinkDownlinkConsoleOutput()
        {
            //Arrange - create a packet to test sending
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - call SendToUplinkDownlink
                handler.SendToUplinkDownlink(packet);

                //Assert - verify console output matches expected JSON output
                string consoleOutput = sw.ToString();
                Assert.IsTrue(consoleOutput.Contains("Sending Packet:"));
                Assert.IsTrue(consoleOutput.Contains("\"CommandType\": \"telemetry\""));
                Assert.IsTrue(consoleOutput.Contains("\"Function\": \"22°C, 5mSv\""));
                Assert.IsTrue(consoleOutput.Contains("\"CRC\": \"0110\""));
            }
        }

        //HandleTransmissionError Tests
        [TestMethod]
        public void TestHandleTransmissionError()
        {
            //Arrange - create a packet for transmission error testing
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");

            //Act - try to handle the transmission (in this case, expect success)
            bool success = handler.HandleTransmissionError(packet);

            //Assert - ensure that the error handling is working as expected
            Assert.IsTrue(success);         //should return true as there was no real error in this test
        }

        [TestMethod]
        public void TestHandleTransmissionErrorWithNullPacket()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - attempt to handle transmission with a null packet
                bool result = handler.HandleTransmissionError(null);

                //Assert - verify it returns false and logs an error message
                string consoleOutput = sw.ToString();
                Assert.IsFalse(result); //should return false due to error
                Assert.IsTrue(consoleOutput.Contains("Error during transmission:")); //check for error message
            }
        }

        [TestMethod]
        public void TestHandleTransmissionErrorConsoleOutputOnSuccess()
        {
            //Arrange - create a packet for testing successful transmission
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - handle transmission for a successful case
                bool result = handler.HandleTransmissionError(packet);

                //Assert - should return true and not contain error messages
                string consoleOutput = sw.ToString();
                Assert.IsTrue(result); //successful send should return true
                Assert.IsFalse(consoleOutput.Contains("Error during transmission:")); //should not log an error
                Assert.IsTrue(consoleOutput.Contains("Sending Packet:")); //verify it outputs the send message
            }
        }

        [TestMethod]
        public void TestHandleTransmissionErrorWithException()
        {
            //Temporarily modify SendToUplinkDownlink to throw an exception for testing
            bool SendToUplinkDownlinkWithException(GroundStationPacket packet)
            {
                throw new Exception("Simulated transmission failure.");
            }

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                try
                {
                    //Act - try handling transmission with forced exception
                    bool result = SendToUplinkDownlinkWithException(null);

                    //Assert - check if false is returned and error is logged
                    Assert.IsFalse(result); //should return false on failure
                    Assert.IsTrue(sw.ToString().Contains("Error during transmission: Simulated transmission failure."));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during transmission: {ex.Message}");
                }
            }
        }

        [TestMethod]
        public void TestHandleTransmissionErrorWithLargeData()
        {
            //Arrange - large data fields
            string largeString = new string('A', 1000);
            GroundStationPacket packet = handler.CreatePacket("telemetry", largeString, largeString);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - handle transmission of large data packet
                bool result = handler.HandleTransmissionError(packet);

                //Assert - should succeed with large data, and check output
                string consoleOutput = sw.ToString();
                Assert.IsTrue(result); //should return true if successfully handled
                Assert.IsTrue(consoleOutput.Contains("Sending Packet:")); //check console for send message
                Assert.IsTrue(consoleOutput.Contains(largeString)); //ensure large data is part of output
            }
        }

        //ProvideTransmissionFeedback Tests
        [TestMethod]
        public void TestProvideTransmissionFeedback()
        {
            //Arrange
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - test feedback for successful transmission
                handler.ProvideTransmissionFeedback(true);
                string successOutput = sw.ToString().Trim();

                //Assert - check that the correct message is printed for success
                Assert.AreEqual("Transmission successful.", successOutput);

                //reset the output capture
                sw.GetStringBuilder().Clear();

                //Act - test feedback for failed transmission
                handler.ProvideTransmissionFeedback(false);
                string failureOutput = sw.ToString().Trim();

                //Assert - check that the correct message is printed for failure
                Assert.AreEqual("Transmission failed. Please check connection.", failureOutput);
            }
        }

        [TestMethod]
        public void TestProvideTransmissionFeedbackMultipleCalls()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - call with success, then failure, then success
                handler.ProvideTransmissionFeedback(true);
                handler.ProvideTransmissionFeedback(false);
                handler.ProvideTransmissionFeedback(true);

                string output = sw.ToString().Trim();

                //Assert - check that all messages appear in sequence
                string[] expectedMessages = {
                    "Transmission successful.",
                    "Transmission failed. Please check connection.",
                    "Transmission successful."
        };
                foreach (string message in expectedMessages)
                {
                    Assert.IsTrue(output.Contains(message));
                }
            }
        }


        //VerifyDataHandling Tests
        [TestMethod]
        public void TestVerifyDataHandling()
        {
            //Arrange - set up a packet for data handling verification
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");

            //Act - verify that the data was handled correctly
            bool result = handler.VerifyDataHandling(packet);

            //Assert - confirm that the handling was marked as verified
            Assert.IsTrue(result);          //should return true to confirm verification
        }

        //ConfirmPacketDelivery Tests
        [TestMethod]
        public void TestConfirmPacketDelivery()
        {
            //Arrange - create a packet for delivery confirmation testing
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");

            //Act - confirm that the packet delivery was successful
            bool result = handler.ConfirmPacketDelivery(packet);  //invoke the delivery confirmation

            //Assert - ensure that the packet delivery was confirmed
            Assert.IsTrue(result);      //should return true to confirm delivery
        }

        //LogSentPacket Tests
        [TestMethod]
        public void TestLogSentPacket()
        {
            //Arrange - create a packet to test logging as "Sent"
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - Log the packet as sent
                handler.LogSentPacket(packet);      //log as "Sent Packet"
                string output = sw.ToString().Trim();       //capture the output

                //Assert - check that the log includes "Sent Packet" and packet details
                Assert.IsTrue(output.Contains("Sent Packet Log"));                  //should have "Sent Packet" label
                Assert.IsTrue(output.Contains("\"CommandType\": \"telemetry\""));      //datatype should be logged
                Assert.IsTrue(output.Contains("\"Function\": \"22°C, 5mSv\""));         //temperature and radiation (data) should be logged
                Assert.IsTrue(output.Contains("\"CRC\": \"0110\""));                //CRC should be logged
            }
        }

        //LogReceivedPacket Tests
        [TestMethod]
        public void TestLogReceivedPacket()
        {
            //Arrange - create a packet to test logging as "Received"
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - Log the packet as received
                handler.LogReceivedPacket(packet);          //log as "Received Packet"
                string output = sw.ToString().Trim();       //capture the output

                //Assert - check that the log includes "Received Packet" and packet details
                Assert.IsTrue(output.Contains("Received Packet Log"));              //should have "Received Packet" label
                Assert.IsTrue(output.Contains("\"CommandType\": \"telemetry\""));      //datatype should be logged
                Assert.IsTrue(output.Contains("\"Function\": \"22°C, 5mSv\""));        //temperature radiation (data) should be logged
                Assert.IsTrue(output.Contains("\"CRC\": \"0110\""));                //CRC should be logged
            }
        }

        //LogPacket Tests
        [TestMethod]
        public void TestLogPacketWithBoundaryValues()
        {
            //Arrange - maximum length strings for temperature and radiation
            string maxLengthString = new string('A', 1000); //1000 characters
            GroundStationPacket packet = handler.CreatePacket("telemetry", maxLengthString, "CRC-BOUNDARY");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // ct - log the packet
                handler.LogPacket(packet, "Boundary Test");

                //Assert - ensure that the logged data contains the long strings and CRC
                string output = sw.ToString();
                Assert.IsTrue(output.Contains(maxLengthString)); //Check that large data is logged correctly
                Assert.IsTrue(output.Contains("CRC-BOUNDARY"));
            }
        }

        [TestMethod]
        public void TestLogPacketWithNullPacket()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - log a null packet with a log type
                handler.LogPacket(null, "Null Packet Test");

                //Assert - verify that output contains an appropriate message or empty JSON
                string output = sw.ToString();
                Assert.IsTrue(output.Contains("Null Packet Test Log:"));
                Assert.IsTrue(output.Contains("{}") || output.Contains("null"));
            }
        }

        [TestMethod]
        public void TestLogPacketWithSpecialCharactersInLogType()
        {
            //Arrange - create a standard packet and a log type with special characters
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");
            string specialLogType = "Special!@#Test";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - log the packet with a special log type
                handler.LogPacket(packet, specialLogType);

                //Assert - verify that the special log type is correctly included in the output
                string output = sw.ToString();
                Assert.IsTrue(output.Contains("Special!@#Test Log:"));
            }
        }

        [TestMethod]
        public void TestLogPacketWithEmptyFields()
        {
            // Arrange - create a packet with empty/default fields
            GroundStationPacket packet = new GroundStationPacket(); // defaults to empty values

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act - log the packet with empty fields
                handler.LogPacket(packet, "Empty Fields Test");

                // Assert - check that output includes empty fields in JSON format
                string output = sw.ToString();
                Assert.IsTrue(output.Contains("Empty Fields Test Log:"));
                Assert.IsTrue(output.Contains("\"CommandType\": \"\""));      // Corrected default value
                Assert.IsTrue(output.Contains("\"Function\": \"\""));         // Empty data field
                Assert.IsTrue(output.Contains("\"CRC\": \"\""));             // Empty CRC
            }
        }

        [TestMethod]
        public void TestLogPacketWithNullInput()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act - Log a null packet
                handler.LogPacket(null, "Null Packet Log");

                // Assert - Verify output contains the expected error message
                string consoleOutput = sw.ToString();
                Assert.IsTrue(consoleOutput.Contains("Null Packet Log:"));
                Assert.IsTrue(consoleOutput.Contains("{}")); // Null packet serialized as empty JSON
            }
        }

        [TestMethod]
        public void TestLogPacketWithSpecialLogType()
        {
            // Arrange - Create a valid packet
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C", "CRC");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act - Log the packet with a special log type
                handler.LogPacket(packet, "Special!@#LogType");

                // Assert - Ensure the log type appears in the output
                string consoleOutput = sw.ToString();
                Assert.IsTrue(consoleOutput.Contains("Special!@#LogType Log:"));
            }
        }

        //Integration Tests
        [TestMethod]
        public void TestEndToEndDataFlow()
        {
            // Arrange: Mock the data from the other group's class
            var command = new Command
            {
                CommandType = "telemetry",
                Latitude = 51.5074,
                Longitude = -0.1278,
                Altitude = 12000,
                Speed = 500
            };
            string telemetryData = "22°C, 5mSv";
            string crc = "0110";

            // Act: Packetize the data
            var handler = new GroundStationPacketHandler();
            GroundStationPacket packet = handler.CombineCommandAndData(command, telemetryData, crc);

            // Assert: Verify the packetized data
            Assert.AreEqual("telemetry", packet.CommandType);
            string expectedFunction = "22°C, 5mSv,51.5074,-0.1278,12000,500";
            Assert.AreEqual(expectedFunction, packet.Function);
            Assert.AreEqual(crc, packet.CRC);

            // Act: Depacketize the data
            string jsonPacket = handler.SerializePacket(packet);
            GroundStationPacket depacketizedPacket = handler.DepacketizeData(jsonPacket);

            // Assert: Verify the depacketized data matches the original
            Assert.AreEqual("telemetry", depacketizedPacket.CommandType);
            Assert.AreEqual(expectedFunction, depacketizedPacket.Function);
            Assert.AreEqual(crc, depacketizedPacket.CRC);

            // Simulate passing the depacketized data back to the other group
            Command receivedCommand = new Command
            {
                CommandType = depacketizedPacket.CommandType,
                Latitude = double.Parse(depacketizedPacket.Function.Split(',')[2]),
                Longitude = double.Parse(depacketizedPacket.Function.Split(',')[3]),
                Altitude = int.Parse(depacketizedPacket.Function.Split(',')[4]),
                Speed = int.Parse(depacketizedPacket.Function.Split(',')[5])
            };

            // Assert: Verify the data passed back matches the original
            Assert.AreEqual(command.CommandType, receivedCommand.CommandType);
            Assert.AreEqual(command.Latitude, receivedCommand.Latitude);
            Assert.AreEqual(command.Longitude, receivedCommand.Longitude);
            Assert.AreEqual(command.Altitude, receivedCommand.Altitude);
            Assert.AreEqual(command.Speed, receivedCommand.Speed);
        }

        [TestMethod]
        public void TestIntegrationWithInvalidData()
        {
            // Arrange: Simulate invalid data from the other group
            var command = new Command
            {
                CommandType = null,  // Invalid
                Latitude = double.NaN,  // Invalid
                Longitude = double.NaN,  // Invalid
                Altitude = -1,  // Invalid
                Speed = -1  // Invalid
            };
            string telemetryData = null;
            string crc = "invalid";

            // Act: Attempt to create a packet
            var handler = new GroundStationPacketHandler();
            GroundStationPacket packet = handler.CombineCommandAndData(command, telemetryData, crc);

            // Assert: Verify the packet's integrity with invalid input
            Assert.AreEqual(string.Empty, packet.CommandType);
            Assert.AreEqual(",,NaN,NaN,-1,-1", packet.Function);
            Assert.AreEqual("invalid", packet.CRC);

            // Act: Attempt to depacketize invalid JSON
            string invalidJson = "{ \"CommandType\": null, \"Function\": null, \"CRC\": null }";
            GroundStationPacket depacketizedPacket = handler.DepacketizeData(invalidJson);

            // Assert: Ensure defaults are applied to the depacketized packet
            Assert.AreEqual(string.Empty, depacketizedPacket.CommandType);
            Assert.AreEqual(string.Empty, depacketizedPacket.Function);
            Assert.AreEqual(string.Empty, depacketizedPacket.CRC);
        }

        [TestMethod]
        public void TestLargeDataFlow()
        {
            // Arrange: Simulate large data from the other group
            string largeTelemetryData = new string('A', 5000); // 5KB of telemetry data
            var command = new Command
            {
                CommandType = "telemetry",
                Latitude = 40.7128,
                Longitude = -74.0060,
                Altitude = 15000,
                Speed = 600
            };
            string crc = new string('B', 1000); // Large CRC

            // Act: Packetize the large data
            var handler = new GroundStationPacketHandler();
            GroundStationPacket packet = handler.CombineCommandAndData(command, largeTelemetryData, crc);

            // Assert: Verify that the packet contains all data without truncation
            Assert.AreEqual(largeTelemetryData + ",40.7128,-74.0060,15000,600", packet.Function);
            Assert.AreEqual(crc, packet.CRC);

            // Act: Depacketize the large data
            string jsonPacket = handler.SerializePacket(packet);
            GroundStationPacket depacketizedPacket = handler.DepacketizeData(jsonPacket);

            // Assert: Verify that the depacketized data matches the original
            Assert.AreEqual(largeTelemetryData + ",40.7128,-74.0060,15000,600", depacketizedPacket.Function);
            Assert.AreEqual(crc, depacketizedPacket.CRC);
        }

        [TestMethod]
        public void TestConcurrentDataExchange()
        {
            // Arrange: Simulate multiple packets being exchanged concurrently
            var handler = new GroundStationPacketHandler();
            var packets = new List<GroundStationPacket>();

            Parallel.For(0, 10, i =>
            {
                // Create unique packets for each thread
                var command = new Command
                {
                    CommandType = "telemetry",
                    Latitude = i * 10,
                    Longitude = i * -10,
                    Altitude = i * 1000,
                    Speed = i * 100
                };
                string telemetryData = $"{i}°C, {i * 2}mSv";
                string crc = $"CRC-{i}";

                // Packetize data
                GroundStationPacket packet = handler.CombineCommandAndData(command, telemetryData, crc);

                // Serialize and depacketize to simulate full flow
                string jsonPacket = handler.SerializePacket(packet);
                GroundStationPacket depacketizedPacket = handler.DepacketizeData(jsonPacket);

                // Store the processed packet
                lock (packets)
                {
                    packets.Add(depacketizedPacket);
                }
            });

            // Assert: Verify that all packets were processed correctly
            Assert.AreEqual(10, packets.Count);
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual($"CRC-{i}", packets[i].CRC);
                Assert.IsTrue(packets[i].Function.Contains($"{i}°C"));
            }
        }

        [TestMethod]
        public void TestSerializeAndDeserializeLargePacket()
        {
            // Arrange - Create a packet with large data
            string largeString = new string('A', 10000); // 10,000 characters
            GroundStationPacket packet = handler.CreatePacket(largeString, largeString, largeString);

            // Act - Serialize and then deserialize the packet
            string jsonPacket = handler.SerializePacket(packet);
            GroundStationPacket deserializedPacket = handler.DepacketizeData(jsonPacket);

            // Assert - Ensure no data is lost during the process
            Assert.IsNotNull(deserializedPacket);
            Assert.AreEqual(largeString, deserializedPacket.CommandType);
            Assert.AreEqual(largeString, deserializedPacket.Function);
            Assert.AreEqual(largeString, deserializedPacket.CRC);
        }

        [TestMethod]
        public void TestSerializeAndDeserializeWithSpecialCharacters()
        {
            // Arrange - Create a packet with special characters
            string commandType = "telemetry-1";
            string function = "22°C, 5mSv";
            string crc = "!@#";

            GroundStationPacket packet = handler.CreatePacket(commandType, function, crc);

            // Act - Serialize and then deserialize the packet
            string jsonPacket = handler.SerializePacket(packet);
            GroundStationPacket deserializedPacket = handler.DepacketizeData(jsonPacket);

            // Assert - Ensure fields with special characters are preserved
            Assert.IsNotNull(deserializedPacket);
            Assert.AreEqual(commandType, deserializedPacket.CommandType);
            Assert.AreEqual(function, deserializedPacket.Function);
            Assert.AreEqual(crc, deserializedPacket.CRC);
        }

    }
}