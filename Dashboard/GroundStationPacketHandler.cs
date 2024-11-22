using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;            //for file handling
using Newtonsoft.Json;      //this lets us convert (serialize) the packet data to JSON format

namespace Dashboard
{
    //this class handles everything related to our Ground Station packets - creating, sending, and logging them
    public class GroundStationPacketHandler
    {
        private readonly string logFilePath;        //path for the log file

        //constructor to ensure the log file exists in the Dashboard directory
        public GroundStationPacketHandler()
        {
            //get the current directory where the app is running (bin/debug/netX.0-windows folder)
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            //navigate up to the "bin" directory by going up two levels
            string dashboardDirectory = Directory.GetParent(Directory.GetParent(appDirectory).Parent.FullName).FullName;

            //combine the path of the "bin" directory with the log file name
            logFilePath = Path.Combine(dashboardDirectory, "GroundStationPacketLogs.txt");

            //check if the log file already exists
            if (!File.Exists(logFilePath))
            {
                //if the file does not exist, create it and close it immediately (avoids locking issues)
                File.Create(logFilePath).Close();
            }
        }

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

        //this function is to combine Command and Data packets
        public GroundStationPacket CombineCommandAndData(Command command, string telemetryData, string crc)
        {
            // Combine telemetry data and command properties into a single Function field
            string functionData = $"{telemetryData},{command.Latitude},{command.Longitude},{command.Altitude},{command.Speed}";

            // Create and return a new packet
            return new GroundStationPacket
            {
                CommandType = command.CommandType,
                Function = functionData,
                CRC = crc
            };
        }

        //this function takes our packet and turns it into JSON format (a format easy for transmission)
        public string SerializePacket(GroundStationPacket packet)
        {
            if (packet == null)
            {
                Console.WriteLine("Serialization error: Packet is null.");
                return string.Empty; // Return an empty string to signify no serialization
            }

            // Converts the packet object to a formatted JSON string
            return JsonConvert.SerializeObject(packet, Newtonsoft.Json.Formatting.Indented);
        }


        //this function simulates sending the packet. here we just print it, but in the real system, we'd send it over a network.
        public bool SendPacket(GroundStationPacket packet)
        {
            if (packet == null)
            {
                Console.WriteLine("Transmission error: Packet is null.");
                return false; // Signal transmission failure
            }

            try
            {
                // Turn the packet into JSON format, which is easy to transmit
                string jsonPacket = SerializePacket(packet);

                // Print the packet to the console, as if we are "sending" it
                Console.WriteLine("Sending Packet:");
                Console.WriteLine(jsonPacket);

                // Log the sent packet
                LogSentPacket(packet);

                return true; // Signal successful transmission
            }
            catch (Exception ex)
            {
                // If something went wrong, print the error message
                Console.WriteLine($"Transmission error: {ex.Message}");

                return false; // Signal that the transmission failed
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
            if (packet == null)
            {
                Console.WriteLine("Error during transmission: Packet is null.");
                return false; // Return false explicitly for null input
            }

            try
            {
                // Try sending the packet and return true if successful
                return SendToUplinkDownlink(packet);
            }
            catch (Exception ex)
            {
                // If there is an error, log it
                Console.WriteLine($"Error during transmission: {ex.Message}");
                return false; // Signal failure
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
            string log = SerializePacket(packet);       //serialize packet into JSON string
            string logEntry = $"{DateTime.Now}: {logType}\n{log}\n";        //format log entry with timestamp and type

            //log to console for debugging
            Console.WriteLine($"{logType} Log:\n{log}");

            //log to file
            try
            {
                File.AppendAllText(logFilePath, logEntry);      //append the log entry to the file
                Console.WriteLine($"Logged {logType} to file: {logFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging {logType}: {ex.Message}");        //handle file logging errors
            }
        }
    }
}