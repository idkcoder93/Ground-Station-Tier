using System;

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
            set { speed = value; } // No validation needed here
        }

        //Added input validation
        public double Latitude
        {
            get { return latitude; }
            set
            {
                if (value < -90.0 || value > 90.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Latitude), "Latitude must be between -90 and 90 degrees.");
                }
                latitude = value;
            }
        }

        // added input validation
        public double Longitude
        {
            get { return longitude; }
            set
            {
                if (value < -180.0 || value > 180.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Longitude), "Longitude must be between -180 and 180 degrees.");
                }
                longitude = value;
            }
        }

        public double Altitude
        {
            get { return altitude; }
            set { altitude = value; } // No validation needed here
        }
    }
}
