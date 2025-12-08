using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TomadaStore.Models.Models;

namespace TomadaStore.SaleAPI.Data
{
    public class ConnectionDB
    {
        public readonly IMongoCollection<Sale> mongoCollection;

        public ConnectionDB(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            mongoCollection = database.GetCollection<Sale>(mongoDBSettings.Value.CollectionName);
        }

        public IMongoCollection<Sale> GetMongoCollection()
        {
            return mongoCollection;
        }
    }
}
