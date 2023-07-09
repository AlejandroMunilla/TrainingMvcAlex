using MongoDB.Driver;
using System;
using System.Diagnostics;
using TrainingAlex.Models;
using MongoDB.Driver.Linq;
using Aspose.Cells;

namespace TrainingAlex.Helpers
{
    public class MongoDbHelper
    {

        public MongoClient dbClient = new MongoClient("mongodb://localhost:27017");

        public MongoDbHelper()
        {
            dbClient = new MongoClient("mongodb://localhost:27017");
        }




        public IMongoQueryable RetrieveDbCollection (string dbName, string collectionName)
        {
            var db = dbClient.GetDatabase(dbName);
            //var collection = db.GetCollection<MiningModel>("Projects");
            var collection = db.GetCollection<MiningModel>(collectionName);

            //var documents = collection.Find(f => true).ToListAsync();
            var filter = Builders<MiningModel>.Filter.Empty;
            var allRestaurants = collection.Find(filter);

            var query =
                from e in collection.AsQueryable<MiningModel>()
                select e;

            foreach (var e in query)
            {
                Debug.WriteLine(e.Company.ToString());
            }
            return query;
        }

   
    }
}
