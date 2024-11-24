using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dashboard
{
    internal class SpaceCraftPacketHandler
    {
        public static SpaceCraftPacket DeserializeSpacePacket(string jsonPacket) 
        {
            //try to deserialize the JSON string into a GroundStationPacket
            SpaceCraftPacket? packet = JsonConvert.DeserializeObject<SpaceCraftPacket>(jsonPacket);

            //check if deserialization failed (result is null), and handle it
            if (packet == null)
            {
                Console.WriteLine("Failed to depacketize data. JSON may be malformed or empty.");
                return new SpaceCraftPacket();       //return an empty packet to avoid null
            }

            return packet;      //return the successfully deserialized packet
        }
    }
}
