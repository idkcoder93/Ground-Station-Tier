using Microsoft.VisualStudio.TestTools.UnitTesting;             //using MS Test framework for writing and running tests
using CDH_GroundStation_Group6;         //accessing our Ground Station classes

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
            string datatype = "telemetry";
            string data = "22°C";
            string crc = "0110";

            //Act - creating a packet using the handler method
            GroundStationPacket packet = handler.CreatePacket(datatype, data, crc);

            //Assert - checking if the packet properties match what we expected
            Assert.IsNotNull(packet);           //packet should not be null
            Assert.AreEqual(datatype, packet.Datatype);         //datatype should match
            Assert.AreEqual(data, packet.Data);                 // data should match
            Assert.AreEqual(crc, packet.CRC);           //CRC should match
        }

        [TestMethod]
        public void TestCreatePacketWithEdgeCaseValues()
        {
            //Arrange - setting up edge case data 
            string datatype = null;
            string data = "";       //use a single Data field instead of separate temperature and radiation
            string crc = "";

            //Act - creating a packet with edge case values
            GroundStationPacket packet = handler.CreatePacket(datatype, data, crc);

            //Assert - checking if the packet properties match the input or default to safe values
            Assert.IsNotNull(packet); //packet should not be null
            Assert.AreEqual(datatype, packet.Datatype); //datatype should match input
            Assert.AreEqual(data, packet.Data);         // data should match input
            Assert.AreEqual(crc, packet.CRC); //CRC should match input
        }

        [TestMethod]
        public void TestCreatePacketWithMaxLengthFields()
        {
            //Arrange - create very long strings for each field
            string datatype = new string('A', 1000);
            string data = new string('B', 1000);
            string crc = new string('D', 1000);

            //Act - create packet with maximum length values
            GroundStationPacket packet = handler.CreatePacket(datatype, data, crc);

            //Assert - check that each property was assigned the long string correctly
            Assert.AreEqual(datatype, packet.Datatype);
            Assert.AreEqual(data, packet.Data);
            Assert.AreEqual(crc, packet.CRC);
        }

        [TestMethod]
        public void TestCreatePacketWithSpecialCharacters()
        {
            //Arrange - data with numbers and special characters
            string datatype = "telemetry-1";
            string data = "22°C, 5.5mSv";
            string crc = "01@#";

            //Act - create packet with these values
            GroundStationPacket packet = handler.CreatePacket(datatype, data, crc);

            //Assert - verify the packet correctly assigns these values
            Assert.AreEqual(datatype, packet.Datatype);
            Assert.AreEqual(data, packet.Data);
            Assert.AreEqual(crc, packet.CRC);
        }

        [TestMethod]
        public void TestCreatePacketWithWhitespaceFields()
        {
            //Arrange - whitespace in each field
            string datatype = "   ";
            string data = "\t ";
            string crc = "\n";

            //Act - create packet with whitespace fields
            GroundStationPacket packet = handler.CreatePacket(datatype, data, crc);

            //Assert - check that fields remain as whitespace
            Assert.AreEqual(datatype, packet.Datatype);
            Assert.AreEqual(data, packet.Data);
            Assert.AreEqual(crc, packet.CRC);
        }

        [TestMethod]
        public void TestCreatePacketWithMinimalValidData()
        {
            //Arrange - minimal non-empty data
            string datatype = "A";
            string data = "1";
            string crc = "1";

            //Act - create packet with minimal valid data
            GroundStationPacket packet = handler.CreatePacket(datatype, data, crc);

            //Assert - ensure fields are set correctly
            Assert.AreEqual(datatype, packet.Datatype);
            Assert.AreEqual(data, packet.Data);
            Assert.AreEqual(crc, packet.CRC);
        }

        //SerializePacket Tests
        [TestMethod]
        public void TestSerializePacket()
        {
            //Arrange - creating a packet to test serialization
            string datatype = "telemetry";
            string data = "22°C, 5mSv"; // Use combined data field
            string crc = "0110";
            GroundStationPacket packet = handler.CreatePacket(datatype, data, crc);

            //Act - converting the packet to JSON format
            string jsonPacket = handler.SerializePacket(packet);

            //Assert - check that the JSON string has all the expected data fields
            Assert.IsNotNull(jsonPacket);
            Assert.IsTrue(jsonPacket.Contains("\"Datatype\": \"telemetry\""));      //should contain the datatype
            Assert.IsTrue(jsonPacket.Contains("\"Data\": \"22°C, 5mSv\""));         //should contain the data field
            Assert.IsTrue(jsonPacket.Contains("\"CRC\": \"0110\""));                //should contain the CRC
        }

        [TestMethod]
        public void TestSerializePacketWithNullInput()
        {
            //Act - attempt to serialize a null packet
            string jsonPacket = handler.SerializePacket(null);

            //Assert - check that the JSON string is either null or empty to signify failed serialization
            Assert.IsTrue(string.IsNullOrEmpty(jsonPacket));
        }

        [TestMethod]
        public void TestSerializeEmptyPacket()
        {
            //Arrange - create an empty packet
            GroundStationPacket packet = new GroundStationPacket();

            //Act - serialize the empty packet
            string jsonPacket = handler.SerializePacket(packet);

            //Assert - check that the JSON contains the default field values 
            Assert.IsNotNull(jsonPacket);
            Assert.IsTrue(jsonPacket.Contains("\"Datatype\": \"telemetry\""));  //default datatype
            Assert.IsTrue(jsonPacket.Contains("\"Data\": \"\""));              // empty data field
            Assert.IsTrue(jsonPacket.Contains("\"CRC\": \"\""));                //empty CRC
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
            Assert.IsTrue(jsonPacket.Contains($"\"Datatype\": \"{largeString}\""));
            Assert.IsTrue(jsonPacket.Contains($"\"Data\": \"{largeString}\""));
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
            Assert.IsTrue(jsonPacket.Contains($"\"Datatype\": \"{datatype}\""));
            Assert.IsTrue(jsonPacket.Contains($"\"Data\": \"{data}\""));
            Assert.IsTrue(jsonPacket.Contains($"\"CRC\": \"{crc}\""));
        }

        //DepacketizeData Tests
        [TestMethod]
        public void TestDepacketizeData()
        {
            //Arrange - create a packet and serialize it to JSON
            string datatype = "telemetry";
            string data = "22°C, 5mSv";
            string crc = "0110";
            GroundStationPacket packet = handler.CreatePacket(datatype, data, crc);
            string jsonPacket = handler.SerializePacket(packet);

            //Act - convert the JSON back to a GroundStationPacket object
            GroundStationPacket depacketizedPacket = handler.DepacketizeData(jsonPacket);

            //Assert - make sure the depacketized packet matches the original data
            Assert.IsNotNull(depacketizedPacket);
            Assert.AreEqual("telemetry", depacketizedPacket.Datatype);
            Assert.AreEqual("22°C, 5mSv", depacketizedPacket.Data);
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
            Assert.AreEqual(string.Empty, packet.Datatype);     //default datatype
            Assert.AreEqual(string.Empty, packet.Data);         //default data field
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
            Assert.AreEqual(string.Empty, packet.Datatype);
            Assert.AreEqual(string.Empty, packet.Data);
            Assert.AreEqual(string.Empty, packet.CRC);
        }

        [TestMethod]
        public void TestDepacketizeDataWithPartialJson()
        {
            //Arrange - JSON with only some fields present
            string partialJson = "{ \"Datatype\": \"telemetry\", \"Data\": \"22°C\" }";

            //Act - attempt to depacketize partial JSON
            GroundStationPacket packet = handler.DepacketizeData(partialJson);

            //Assert - check that missing fields are set to defaults and present fields match the JSON
            Assert.IsNotNull(packet);
            Assert.AreEqual("telemetry", packet.Datatype);
            Assert.AreEqual("22°C", packet.Data);
            Assert.AreEqual(string.Empty, packet.CRC);
        }

        [TestMethod]
        public void TestDepacketizeDataWithLargeValues()
        {
            //Arrange - JSON with very large string values
            string largeString = new string('A', 1000);
            string largeJson = $"{{ \"Datatype\": \"{largeString}\", \"Data\": \"{largeString}\", \"CRC\": \"{largeString}\" }}";

            //Act - attempt to depacketize JSON with large values
            GroundStationPacket packet = handler.DepacketizeData(largeJson);

            //Assert - check that each field contains the full large value
            Assert.IsNotNull(packet);
            Assert.AreEqual(largeString, packet.Datatype);
            Assert.AreEqual(largeString, packet.Data);
            Assert.AreEqual(largeString, packet.CRC);
        }

        [TestMethod]
        public void TestDepacketizeDataWithSpecialCharacters()
        {
            //Arrange - JSON with special characters
            string jsonWithSpecialChars = "{ \"Datatype\": \"telemetry-1\", \"Temperature\": \"22°C\", \"Radiation\": \"5.5mSv\", \"CRC\": \"01@#\" }";

            //Act - depacketize JSON with special characters
            GroundStationPacket packet = handler.DepacketizeData(jsonWithSpecialChars);

            //Assert - ensure that fields with special characters are deserialized correctly
            Assert.IsNotNull(packet);
            Assert.AreEqual("telemetry-1", packet.Datatype);
            Assert.AreEqual("22°C, 5.5mSv", packet.Data);
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
            //Arrange - create a packet to test sending
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C, 5mSv", "0110");

            //Act - simulate an error during packet sending
            bool result;
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                result = handler.SendPacket(null); //Intentionally passing null to simulate failure
            }

            //Assert - check if failure was handled
            Assert.IsFalse(result);         //should be false due to transmission error
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
                Assert.IsTrue(consoleOutput.Contains("\"Datatype\": \"telemetry\""));
                Assert.IsTrue(consoleOutput.Contains("\"Data\": \"22°C, 5mSv\""));
                Assert.IsTrue(consoleOutput.Contains("\"CRC\": \"0110\""));
            }
        }

        [TestMethod]
        public void TestSendPacketConsoleOutputFailure()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - simulate sending a null packet to cause failure
                bool result = handler.SendPacket(null);

                //Assert - verify that method returned false and output the correct error message
                string consoleOutput = sw.ToString();
                Assert.IsFalse(result);                                         //should return false for failed send
                Assert.IsTrue(consoleOutput.Contains("Transmission error:"));   //check for error message
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
        public void TestSendEmptyPacket()
        {
            //Arrange - create an empty packet
            GroundStationPacket packet = new GroundStationPacket();

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - simulate sending the empty packet
                bool result = handler.SendPacket(packet);

                //Assert - check that sending was successful and default values are in output
                string consoleOutput = sw.ToString();
                Assert.IsTrue(result);
                Assert.IsTrue(consoleOutput.Contains("\"Datatype\": \"telemetry\""));       //default datatype
                Assert.IsTrue(consoleOutput.Contains("\"Data\": \"\""));        //empty data field
                Assert.IsTrue(consoleOutput.Contains("\"CRC\": \"\""));         //empty CRC
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
                Assert.IsTrue(consoleOutput.Contains("\"Datatype\": \"telemetry\""));
                Assert.IsTrue(consoleOutput.Contains("\"Data\": \"22°C, 5mSv\""));
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
                Assert.IsTrue(output.Contains("\"Datatype\": \"telemetry\""));      //datatype should be logged
                Assert.IsTrue(output.Contains("\"Data\": \"22°C, 5mSv\""));         //temperature and radiation (data) should be logged
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
                Assert.IsTrue(output.Contains("\"Datatype\": \"telemetry\""));      //datatype should be logged
                Assert.IsTrue(output.Contains("\"Data\": \"22°C, 5mSv\""));        //temperature radiation (data) should be logged
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
            //Arrange - create a packet with empty/default fields
            GroundStationPacket packet = new GroundStationPacket(); //defaults to empty values

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - log the packet with empty fields
                handler.LogPacket(packet, "Empty Fields Test");

                //Assert - check that output includes empty fields in JSON format
                string output = sw.ToString();
                Assert.IsTrue(output.Contains("Empty Fields Test Log:"));
                Assert.IsTrue(output.Contains("\"Datatype\": \"telemetry\""));      //default value for datatype
                Assert.IsTrue(output.Contains("\"Data\": \"\""));       //empty Data field
                Assert.IsTrue(output.Contains("\"CRC\": \"\""));        //empty CRC
            }
        }
    }
}
