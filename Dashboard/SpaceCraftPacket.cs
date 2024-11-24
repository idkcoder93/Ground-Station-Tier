using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard
{
    internal class SpaceCraftPacket
    {

        //packet properties as per the new structure
        public string Datetime { get; set; }
        public string DataType { get; set; }
        public string Data { get; set; }
        public string CRC { get; set; }

        //default constructor
        public SpaceCraftPacket()
        {
            Datetime = DateTime.Now.ToString(new CultureInfo("en-US"));
            DataType = string.Empty;         //initialize as empty
            Data = string.Empty;            //initialize as empty
            CRC = string.Empty;                 //initialize as empty
        }


        public SpaceCraftPacket(string dataType, string data, string crc)
        {
            Datetime = DateTime.Now.ToString(new CultureInfo("en-US"));
            DataType = dataType;
            Data = data;
            CRC = crc;
        }
    }
}