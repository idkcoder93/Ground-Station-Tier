using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ground_station.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService()
        {
            var connectionString = "mongodb+srv://alexfridman93:8aa6IITgDET8CaTx@groundstationdb.mj8g0.mongodb.net/?retryWrites=true&w=majority&appName=GroundStationDB";
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("GroundStationDB");
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        public async Task SaveDataAsync<T>(string collectionName, T data)
        {
            var collection = _database.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(data);
        }
    }
}
