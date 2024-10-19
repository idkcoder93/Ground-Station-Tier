namespace ground_station.Models
{
    public class CommandPacket
    {
        public string Command { get; set; }
        public Dictionary<string, string> Parameters { get; set; }

        public CommandPacket()
        {
            Command = string.Empty;
            Parameters = new Dictionary<string, string>();
        }
    }
}
