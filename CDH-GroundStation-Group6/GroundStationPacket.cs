using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;     //as agreed on teams, this is for en-US culture info

namespace CDH_GroundStation_Group6
{
    //the definition of the Packet class for "REST"-based communication
    public class GroundStationPacket
    {
        //the date and time when the packet was created or transmitted (formatted for en-US)
        public string Datetime { get; set; }

        //type of the data (like telemetry)
        public string Datatype { get; set; }

        //explicit fields for sensor data
        public string Temperature { get; set; }        //temperature data
        public string Radiation { get; set; }          //radiation data

        //CRC (aka Cyclic Redundancy Check) for error-checking as agreed
        public string CRC { get; set; }

        //constructor to initialize the packet with default values
        public GroundStationPacket()
        {
            //set the current time with en-US formatting
            Datetime = DateTime.Now.ToString(new CultureInfo("en-US"));
            Datatype = "telemetry";         //default to telemetry data
            Temperature = string.Empty;     //default to an empty string
            Radiation = string.Empty;       //default to an empty string
            CRC = string.Empty;             //default to an empty CRC
        }
    }
}
