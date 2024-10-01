using MongoDB.Bson;
using MongoDB.Driver;

namespace CDH_GroundStation_Group6
{
    internal class Database
    {
        // Read-only class-level field
        private readonly MongoClient dbClient;
        private readonly IMongoCollection<BsonDocument> userinfo;

        // Initialize it in the constructor
        public Database()
        {
            dbClient = new MongoClient("mongodb://localhost:27500");
            IMongoDatabase database_schema = dbClient.GetDatabase("GroundStationDB");

            // Initialize userinfo with the 'Users' collection
            userinfo = database_schema.GetCollection<BsonDocument>("Users");
        }

        // Searches through Users collection to see if results match
        public Boolean searchUserInDB(string username, string password)
        {
            // Create a filter to match the username and password fields
            var filter = Builders<BsonDocument>.Filter.Eq("username", username) &
                         Builders<BsonDocument>.Filter.Eq("password", password);

            // Find the user with the matching credentials
            var user = userinfo.Find(filter).FirstOrDefault();

            if (user == null) {return false;}

            else { return true;}
        }
    }
}

