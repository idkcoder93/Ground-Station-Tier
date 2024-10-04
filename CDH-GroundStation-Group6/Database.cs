using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace CDH_GroundStation_Group6
{
    internal class Database
    {
        // Read-only class-level field
        private readonly string key = "key1234567890123"; // 16-BYTE key
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

        // Searches through Users collection to see if the results match
        public Boolean SearchUserInDB(User currentUser)
        {
            // Get the 'Users' collection from the database
            var userCollection = database.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.Eq("username", currentUser.Username);
            var user = userCollection.Find(filter).FirstOrDefault();

            if (user == null)
            {
                return false;  // User not found
            }

            string encryptedPassword = user["password"].AsString;

            // Decrypt password and check if it matches
            string decryptedPassword = Encrpytion.Decrypt(encryptedPassword, this.key);

            // Return true if passwords match, otherwise false
            return decryptedPassword == currentUser.Password;
        }

        //public void CreateUser(User user)
        //{

        //}

    }
}

