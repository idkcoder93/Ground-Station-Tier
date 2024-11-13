using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CDH_GroundStation_Group6
{
    public class CommandPacket
    {
        public string Command { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }

    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Create and run the ASP.NET Core web application
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

            var hostTask = app.RunAsync();

            // Run the Windows Forms application
            ApplicationConfiguration.Initialize();
            Application.Run(new SignInPage());

            // Wait for the web host to complete
            hostTask.Wait();
        }
    }
}
