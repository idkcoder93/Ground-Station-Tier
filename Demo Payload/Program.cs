using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddSingleton<MongoDbService>();

// Add logging services
builder.Services.AddLogging();

var app = builder.Build();

// Define the /api/receive endpoint
app.MapPost("/api/receive", async (CommandPacket commandPacket, MongoDbService mongoDbService, ILogger<Program> logger) =>
{
    logger.LogInformation("Received data:");
    logger.LogInformation($"Command: {commandPacket.Command}");
    logger.LogInformation($"Parameters: {string.Join(", ", commandPacket.Parameters.Select(kv => $"{kv.Key}: {kv.Value}"))}");
    logger.LogInformation($"Source: {commandPacket.Source}");
    logger.LogInformation($"Destination: {commandPacket.Destination}");

    try
    {
        // Save data to MongoDB
        await mongoDbService.SaveDataAsync("ReceivedData", commandPacket);
        logger.LogInformation("Data saved to MongoDB successfully.");
        return Results.Ok("Data saved successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError($"Failed to save data to MongoDB: {ex.Message}");
        return Results.Problem("Failed to save data.", statusCode: 500);
    }
});

app.Run();

public class MongoDbService
{
    private readonly IMongoDatabase _database;
    public MongoDbService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDbConnection") ??
                               Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("MongoDB connection string is not configured.");
        }

        var client = new MongoClient(connectionString);

        try
        {
            // Try a quick ping to verify the connection
            _database = client.GetDatabase("GroundStationDB");  // Use _database instead of a local variable
            var pingCommand = new BsonDocument("ping", 1);
            var result = _database.RunCommand<BsonDocument>(pingCommand);
            Console.WriteLine("MongoDB connection successful!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MongoDB connection failed: {ex.Message}");
        }
    }


    public async Task SaveDataAsync<T>(string collectionName, T data)
    {
        var collection = _database.GetCollection<T>(collectionName);
        await collection.InsertOneAsync(data);
    }
}

// CommandPacket Class (Keep this as is or refactor as needed)
public class CommandPacket
{
    public string Command { get; set; }
    public Dictionary<string, string> Parameters { get; set; }
    public string Source { get; set; }
    public string Destination { get; set; }
}
