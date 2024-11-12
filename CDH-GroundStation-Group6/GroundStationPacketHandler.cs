using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;      //this lets us convert (serialize) the packet data to JSON format

namespace CDH_GroundStation_Group6
{
    //this class handles everything related to our Ground Station packets - creating, sending, and logging them
    public class GroundStationPacketHandler
    {
        //this function makes a new packet with data we provide, like temperature and radiation levels
        public GroundStationPacket CreatePacket(string commandType, string function, string crc)
        {
            return new GroundStationPacket
            {
                CommandType = commandType,
                Function = function,
                CRC = crc
            };
        }

        //this function takes our packet and turns it into JSON format (a format easy for transmission)
        public string SerializePacket(GroundStationPacket packet)
        {
            //converts the packet object to a formatted JSON string
            return JsonConvert.SerializeObject(packet, Formatting.Indented);
        }

        //this function simulates sending the packet. here we just print it, but in the real system, we'd send it over a network.
        public bool SendPacket(GroundStationPacket packet)
        {
            try
            {
                //turn the packet into JSON format, which is easy to transmit
                string jsonPacket = SerializePacket(packet);

                //print the packet to the console, as if we are "sending" it
                Console.WriteLine("Sending Packet:");
                Console.WriteLine(jsonPacket);

                return true;        //say everything went okay (successful transmission)
            }
            catch (Exception ex)
            {
                //if something went wrong, print the error message
                Console.WriteLine($"Transmission error: {ex.Message}");

                return false;       //signal that the transmission failed.
            }
        }

        //this function takes a JSON string and converts it back into a packet (depacketizing)
        public GroundStationPacket DepacketizeData(string jsonPacket)
        {
            //try to deserialize the JSON string into a GroundStationPacket
            GroundStationPacket? packet = JsonConvert.DeserializeObject<GroundStationPacket>(jsonPacket);

            //check if deserialization failed (result is null), and handle it
            if (packet == null)
            {
                Console.WriteLine("Failed to depacketize data. JSON may be malformed or empty.");
                return new GroundStationPacket();       //return an empty packet to avoid null
            }

            return packet;      //return the successfully deserialized packet
        }

        //this function simulates sending data to uplink/downlink
        public bool SendToUplinkDownlink(GroundStationPacket packet)
        {
            //use the SendPacket function to "send" the data
            return SendPacket(packet);
        }

        //this function handles any transmission errors and logs them
        public bool HandleTransmissionError(GroundStationPacket packet)
        {
            try
            {
                //try sending the packet and return true if successful
                return SendToUplinkDownlink(packet);
            }
            catch (Exception ex)
            {
                //if there is an error, log it
                Console.WriteLine($"Error during transmission: {ex.Message}");
                return false;       //signal failure
            }
        }

        //this function provides feedback on whether the transmission was successful
        public void ProvideTransmissionFeedback(bool success)
        {
            //print a message based on success or failure
            if (success)
            {
                Console.WriteLine("Transmission successful.");
            }
            else
            {
                Console.WriteLine("Transmission failed. Please check connection.");
            }
        }

        //this function simulates verifying that uplink/downlink handled the data correctly
        public bool VerifyDataHandling(GroundStationPacket packet)
        {
            //simulate verification process and log it
            Console.WriteLine("Data handling verified by uplink/downlink.");
            return true;        //assume verification was successful
        }

        //this function simulates receiving confirmation of packet delivery
        public bool ConfirmPacketDelivery(GroundStationPacket packet)
        {
            //simulate confirmation receipt
            Console.WriteLine("Packet delivery confirmed.");
            return true;        //assume delivery confirmation was received
        }

        //this function logs packets that were sent for debugging purposes
        public void LogSentPacket(GroundStationPacket packet)
        {
            //log the packet with a "Sent Packet" label
            LogPacket(packet, "Sent Packet");
        }

        //this function logs packets that were received for debugging purposes
        public void LogReceivedPacket(GroundStationPacket packet)
        {
            //log the packet with a "Received Packet" label
            LogPacket(packet, "Received Packet");
        }

        //this internal function logs any packet with a specified log type  
        public void LogPacket(GroundStationPacket packet, string logType)
        {
            //convert the packet to JSON, making it easy to read in the log
            string log = SerializePacket(packet);

            //print the log type and the JSON-formatted packet
            Console.WriteLine($"{logType} Log:\n{log}");
        }
    }
}
