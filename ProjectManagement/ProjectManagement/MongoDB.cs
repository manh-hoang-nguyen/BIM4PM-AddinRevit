using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
namespace ProjectManagement
{
    public class MongoDB
    {
        private static MongoDB instance;

        public static MongoDB Instance { get => instance; set => instance = value; }

        private MongoDB() { }
        private string link = "mongodb+srv://Manh-Hoang:123456A@cluster0-gkgpa.gcp.mongodb.net/test?retryWrites=true&w=majority";
        public IMongoCollection<BsonDocument> Connection()
        {
            
           
            var client = new MongoClient(link);
            var database = client.GetDatabase("training_nodejs");

            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("items");

            return collection;
        }
    }
}
