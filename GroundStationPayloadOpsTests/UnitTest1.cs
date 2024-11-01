using Microsoft.VisualStudio.TestTools.UnitTesting;             //using MS Test framework for writing and running tests
using CDH_GroundStation_Group6;         //accessing our Ground Station classes

namespace GroundStationPayloadOpsTests
{
    //using AAA
    [TestClass]     //marks this class as a test class for MS Test
    public class GroundStationPacketHandlerTests
    {
        private GroundStationPacketHandler handler = new GroundStationPacketHandler();          //initializes handler immediately

        [TestMethod]    //marks this method as a test
        public void TestCreatePacket()
        {
            //Arrange - setting up the data we will use to create a packet
            string datatype = "telemetry";
            string temperature = "22°C";
            string radiation = "5mSv";
            string crc = "0110";

            //Act - creating a packet using the handler method
            GroundStationPacket packet = handler.CreatePacket(datatype, temperature, radiation, crc);

            //Assert - checking if the packet properties match what we expected
            Assert.IsNotNull(packet);           //packet should not be null
            Assert.AreEqual(datatype, packet.Datatype);         //datatype should match
            Assert.AreEqual(temperature, packet.Temperature);       //temperature should match
            Assert.AreEqual(radiation, packet.Radiation);       //radiation level should match
            Assert.AreEqual(crc, packet.CRC);           //CRC should match
        }

        [TestMethod]
        public void TestSerializePacket()
        {
            //Arrange - creating a packet to test serialization
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C", "5mSv", "0110");

            //Act - converting the packet to JSON format
            string jsonPacket = handler.SerializePacket(packet);

            //Assert - check that the JSON string has all the expected data fields
            Assert.IsNotNull(jsonPacket);       //JSON result should not be null
            Assert.IsTrue(jsonPacket.Contains("\"Datatype\": \"telemetry\""));         //should contain the datatype
            Assert.IsTrue(jsonPacket.Contains("\"Temperature\": \"22°C\""));           //should contain the temperature
            Assert.IsTrue(jsonPacket.Contains("\"Radiation\": \"5mSv\""));             //should contain the radiation level
            Assert.IsTrue(jsonPacket.Contains("\"CRC\": \"0110\""));                   //should contain the CRC
        }

        [TestMethod]
        public void TestDepacketizeData()
        {
            //Arrange - create a packet and serialize it to JSON
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C", "5mSv", "0110");
            string jsonPacket = handler.SerializePacket(packet);

            //Act - convert the JSON back to a GroundStationPacket object
            GroundStationPacket depacketizedPacket = handler.DepacketizeData(jsonPacket);

            //Assert - make sure the depacketized packet matches the original data
            Assert.IsNotNull(depacketizedPacket);                         //packet shouldn't be null
            Assert.AreEqual("telemetry", depacketizedPacket.Datatype);    //datatype should match
            Assert.AreEqual("22°C", depacketizedPacket.Temperature);      //temperature should match
            Assert.AreEqual("5mSv", depacketizedPacket.Radiation);        //radiation should match
            Assert.AreEqual("0110", depacketizedPacket.CRC);              //CRC should match
        }

        [TestMethod]
        public void TestSendPacketSuccess()
        {
            //Arrange - create a packet to test sending
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C", "5mSv", "0110");

            //Act - simulate sending the packet
            bool result = handler.SendPacket(packet);

            //Assert - check if sending was reported as successful
            Assert.IsTrue(result);          //should be true if the packet was "sent" successfully
        }

        [TestMethod]
        public void TestHandleTransmissionError()
        {
            //Arrange - create a packet for transmission error testing
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C", "5mSv", "0110");

            //Act - try to handle the transmission (in this case, expect success)
            bool success = handler.HandleTransmissionError(packet);

            //Assert - ensure that the error handling is working as expected
            Assert.IsTrue(success);         //should return true as there was no real error in this test
        }

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
        public void TestVerifyDataHandling()
        {
            //Arrange - set up a packet for data handling verification
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C", "5mSv", "0110");

            //Act - verify that the data was handled correctly
            bool result = handler.VerifyDataHandling(packet);

            //Assert - confirm that the handling was marked as verified
            Assert.IsTrue(result);          //should return true to confirm verification
        }

        [TestMethod]
        public void TestConfirmPacketDelivery()
        {
            //Arrange - create a packet for delivery confirmation testing
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C", "5mSv", "0110");

            //Act - confirm that the packet delivery was successful
            bool result = handler.ConfirmPacketDelivery(packet);  //invoke the delivery confirmation

            //Assert - ensure that the packet delivery was confirmed
            Assert.IsTrue(result);      //should return true to confirm delivery
        }

        [TestMethod]
        public void TestLogSentPacket()
        {
            //Arrange - create a packet to test logging as "Sent"
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C", "5mSv", "0110");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - Log the packet as sent
                handler.LogSentPacket(packet);      //log as "Sent Packet"
                string output = sw.ToString().Trim();       //capture the output

                //Assert - check that the log includes "Sent Packet" and packet details
                Assert.IsTrue(output.Contains("Sent Packet Log"));                  //should have "Sent Packet" label
                Assert.IsTrue(output.Contains("\"Datatype\": \"telemetry\""));      //datatype should be logged
                Assert.IsTrue(output.Contains("\"Temperature\": \"22°C\""));        //temperature should be logged
                Assert.IsTrue(output.Contains("\"Radiation\": \"5mSv\""));          //radiation should be logged
                Assert.IsTrue(output.Contains("\"CRC\": \"0110\""));                //CRC should be logged
            }
        }

        [TestMethod]
        public void TestLogReceivedPacket()
        {
            //Arrange - create a packet to test logging as "Received"
            GroundStationPacket packet = handler.CreatePacket("telemetry", "22°C", "5mSv", "0110");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                //Act - Log the packet as received
                handler.LogReceivedPacket(packet);          //log as "Received Packet"
                string output = sw.ToString().Trim();       //capture the output

                //Assert - check that the log includes "Received Packet" and packet details
                Assert.IsTrue(output.Contains("Received Packet Log"));              //should have "Received Packet" label
                Assert.IsTrue(output.Contains("\"Datatype\": \"telemetry\""));      //datatype should be logged
                Assert.IsTrue(output.Contains("\"Temperature\": \"22°C\""));        //temperature should be logged
                Assert.IsTrue(output.Contains("\"Radiation\": \"5mSv\""));          //radiation should be logged
                Assert.IsTrue(output.Contains("\"CRC\": \"0110\""));                //CRC should be logged
            }
        }
    }
}
