using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;     //as agreed on teams, this is for en-US culture info

namespace Dashboard
{
    public class GroundStationPacket
    {
        //packet properties as per the new structure
        public string Datetime { get; set; }
        public string CommandType { get; set; }
        public string Function { get; set; }
        public string CRC { get; set; }

        //default constructor
        public GroundStationPacket()
        {
            Datetime = DateTime.Now.ToString(new CultureInfo("en-US"));
            CommandType = string.Empty;         //initialize as empty
            Function = string.Empty;            //initialize as empty
            CRC = string.Empty;                 //initialize as empty
        }

        //constructor with parameters for creating a packet
        public GroundStationPacket(string commandType, string function, string crc)
        {
            Datetime = DateTime.Now.ToString(new CultureInfo("en-US"));
            CommandType = commandType;
            Function = function;
            CRC = crc;
        }
    }
}