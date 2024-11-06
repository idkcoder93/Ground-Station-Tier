using System;
using System.Collections.Generic;

namespace ground_station.Models
{
    public class CommandPacket
    {
        public string Command { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string Source { get; set; }  // e.g., "Spacecraft", "ScientificOperation"
        public string Destination { get; set; }  // e.g., "PayloadOps", "GroundStation"
        public bool IsValid { get; set; } = true; // Property to indicate validity

        public CommandPacket(string command, Dictionary<string, string>? parameters = null, string source = "", string destination = "")
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                Command = string.Empty; // or null, depending on your design
            }
            else
            {
                Command = command;
            }
            Parameters = parameters ?? new Dictionary<string, string>(); // Default to empty dictionary
            Source = source;
            Destination = destination;
        }
    }
}
