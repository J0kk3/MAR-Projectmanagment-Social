using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
//project namespaces
using Domain;

namespace Persistence
{
    public class DataContext
    {
        readonly IMongoDatabase _db;
        readonly IConfiguration Configuration;

        public DataContext(IConfiguration config)
        {
            var mongodbUser = config["mongodbUser"];
            var mongodbPw = config["mongodbPw"];
            var mongodbCluster = config["mongodbCluster"];
            var connectionString = $"mongodb+srv://{mongodbUser}:{mongodbPw}@{mongodbCluster}.aa0muro.mongodb.net/?retryWrites=true&w=majority";

            var client = new MongoClient(connectionString);
            _db = client.GetDatabase("ProjectManagement");
        }

        //MongoDB Collections
        public IMongoCollection<Project> Projects => _db.GetCollection<Project>("Projects");
        public IMongoCollection<Notification> Notifications => _db.GetCollection<Notification>("Notifications");
    }
}