using System.Configuration;
using MongoDB.Driver;

namespace ReportsService.Source.Mongo
{
    
    public class MongoClient
    {
        private MongoClient()
        {
            // Connect to Mongo Atlas reports database
            var connectionString = ConfigurationManager.AppSettings["MongoUrl"];
            Client = new MongoDB.Driver.MongoClient(connectionString);
            Db = Client.GetDatabase("reportsDB");
        }

        // Properties
        public static MongoClient Instance { get; } = new MongoClient();
        
        public IMongoDatabase Db { get; }

        private MongoDB.Driver.MongoClient Client { get; }

    }
}