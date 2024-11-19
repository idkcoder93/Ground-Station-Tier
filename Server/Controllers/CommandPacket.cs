// CommandPacket.cs
namespace Server.Models // Choose an appropriate namespace name, like "Models"
{
    public class CommandPacket
    {
        public SensorData SensorData { get; set; }
        public Settings Settings { get; set; }
    }

    public class SensorData
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public string Status { get; set; }
    }

    public class Settings
    {
        public double TemperatureSetting { get; set; }
        public double HumiditySetting { get; set; }
        public string PowerSetting { get; set; }
    }
}
