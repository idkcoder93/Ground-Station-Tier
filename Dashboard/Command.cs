using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard
{
    class Command
    {
        private string commandType;
        private double speed, latitude, longitude, altitude;

        public string CommandType
        {
            get { return commandType; }
            set { commandType = value; }

        }

        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public double Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }
    }
}
