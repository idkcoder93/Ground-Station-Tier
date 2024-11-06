using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Endpoint to receive data
app.MapPost("/api/receive", (CommandPacket commandPacket) =>
{
    Console.WriteLine("Received data:");
    Console.WriteLine($"Command: {commandPacket.Command}");
    Console.WriteLine($"Parameters: {string.Join(", ", commandPacket.Parameters.Select(kv => $"{kv.Key}: {kv.Value}"))}");
    Console.WriteLine($"Source: {commandPacket.Source}");
    Console.WriteLine($"Destination: {commandPacket.Destination}");
    return Results.Ok();
});

app.Run();

public class CommandPacket
{
    public string Command { get; set; }
    public Dictionary<string, string> Parameters { get; set; }
    public string Source { get; set; }
    public string Destination { get; set; }
}
