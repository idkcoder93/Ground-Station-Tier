using ground_station.Services;
using ground_station.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ground_station
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ground Station API", Version = "v1" });
            });

            // Register services
            services.AddSingleton<UplinkCommunicationService>();
            services.AddSingleton<LoggingAndMonitoringService>();
            services.AddSingleton<DownlinkCommunicationService>();
            services.AddHttpClient<DataForwardingService>(); // Register forwarding service

            // Add MongoDbService for saving data to MongoDB
            services.AddSingleton<MongoDbService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ground Station API v1"));
            }

            app.UseRouting();
            app.UseAuthorization();

            // Define /api/uplink endpoint here, use UseEndpoints instead of MapPost directly
            app.UseEndpoints(endpoints =>
            {
                // Define POST route in UseEndpoints method
                endpoints.MapPost("/api/uplink", async (CommandPacket commandPacket, DataForwardingService forwardingService, MongoDbService mongoDbService) =>
                {
                    Console.WriteLine("Uplink received command packet:");
                    Console.WriteLine($"Command: {commandPacket.Command}");
                    Console.WriteLine($"Source: {commandPacket.Source}");
                    Console.WriteLine($"Destination: {commandPacket.Destination}");

                    var destinationUrl = "http://localhost:5297"; // Update with your actual URL

                    try
                    {
                        // Forward data
                        if (await forwardingService.ForwardDataAsync(destinationUrl, commandPacket))
                        {
                            // Save to MongoDB
                            await mongoDbService.SaveDataAsync("commandPackets", commandPacket);
                            Console.WriteLine("Data saved to MongoDB successfully.");
                            return Results.Ok("Command forwarded and saved successfully.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }

                    return Results.Problem("Failed to forward command.", statusCode: 500);
                });

                // Use Controllers for other routes
                endpoints.MapControllers();
            });
        }
    }
}


namespace ground_station
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
