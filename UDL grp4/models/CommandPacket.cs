using System;
using System.Collections.Generic;

namespace ground_station.Models
{
    public class CommandPacket
    {
        public required string Command { get; set; }
        public required Dictionary<string, string> Parameters { get; set; }
        public bool IsValid { get; set; } = true; // Property to indicate validity

        public CommandPacket(string command, Dictionary<string, string>? parameters = null)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Parameters = parameters ?? new Dictionary<string, string>(); // Default to empty dictionary
        }

    }
}

