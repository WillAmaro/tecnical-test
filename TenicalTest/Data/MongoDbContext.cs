
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace BlazorTecnicalTest.Data
{
    public class MongoDbContext
    {

        public IMongoDatabase DataBase { get; }

        public MongoDbContext(IConfiguration config)
        {
            var connectionString = config["MongoDb:ConnectionString"];
            var databaseName = config["MongoDb:Database"];
            var client = new MongoClient(connectionString);
            DataBase = client.GetDatabase(databaseName);
        }

    }
}
