using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;
using System;

namespace Dashboard
{
    internal class Database
    {
        // Read-only class-level field
        //private readonly string key = "key1234567890123"; // 16-BYTE key
        private const string connectionUri = "mongodb+srv://alexfridman93:8aa6IITgDET8CaTx@groundstationdb.mj8g0.mongodb.net/?retryWrites=true&w=majority&appName=GroundStationDB";

        // MongoClient and database object as class-level fields
        private readonly MongoClient client;
        private readonly IMongoDatabase database;

        // Initialize it in the constructor
        public Database()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionUri);

            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Create a new client and connect to the server
            client = new MongoClient(settings);

            // Get the database from the server
            database = client.GetDatabase("GroundStationDB");
        }
        public async void CommandsSentStored(Command c)
        {
            var collection = database.GetCollection<Command>("InCommands");

            try
            {
                await collection.InsertOneAsync(new Command
                {
                    CommandType = c.CommandType,
                    Longitude = c.Longitude,
                    Latitude = c.Latitude,
                    Altitude = c.Altitude,
                    Speed = c.Speed
                });

                // Optionally, you can log or notify the user of a successful operation
                Console.WriteLine("Command successfully stored in the database.");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while storing the command: {ex.Message}");

                // Optionally, notify the user of the failure
                MessageBox.Show("Failed to connect to database", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

