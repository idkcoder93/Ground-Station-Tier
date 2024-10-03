using MongoDB.Bson;
using MongoDB.Driver;
//using Encryption;

namespace CDH_GroundStation_Group6
{
    internal class Database
    {
        // Read-only class-level field
        private readonly MongoClient dbClient;
        private readonly IMongoCollection<BsonDocument> userinfo;
        private readonly string key = "key1234567890123"; // 16-BYTE key

        // Initialize it in the constructor
        public Database()
        {
            dbClient = new MongoClient("mongodb+srv://alexfridman93:8aa6IITgDET8CaTx@groundstationdb.mj8g0.mongodb.net/?retryWrites=true&w=majority&appName=GroundStationDB");
            IMongoDatabase database_schema = dbClient.GetDatabase("GroundStationDB");

            // Initialize userinfo with the 'Users' collection
            userinfo = database_schema.GetCollection<BsonDocument>("Users");
        }

        // Searches through Users collection to see if results match
        public Boolean SearchUserInDB(string username, string password)
        {

            // Fetch user from database by username
            var filter = Builders<BsonDocument>.Filter.Eq("username", username);
            var user = userinfo.Find(filter).FirstOrDefault();

            if (user == null)
            {
                return false;  // User not found
            }

            // Get the stored encrypted password and IV from the database
            string encryptedPassword = user["password"].AsString;

            // Decrypt Password and check if matches
            string decryptedPassword = Encrpytion.Decrypt(encryptedPassword, this.key);
            if (decryptedPassword == password) { return true; }
            else { return false; }
        }

        //public void CreateUser(User user)
        //{

        //}

    }
}

