using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace QSearch.Core.Impl
{
    public class MongoDbBasedCacheService : ICacheService
    {
        string connectionstr, databaseName;

        public MongoDbBasedCacheService()
        {
            this.connectionstr = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            
            //take database name from connection string
            this.databaseName = MongoUrl.Create(this.connectionstr).DatabaseName;
        }

        public void addToCache(SearchQuery query, List<SearchResult> result)
        {
            var client = new MongoClient(this.connectionstr);
            var db = client.GetDatabase(this.databaseName);

            var collection = db.GetCollection<BsonDocument>("searchresults");
            var filter = new BsonDocument();

            using (var cursor = collection.Find(filter))
            {
                while (cursor.MoveNext())
                {
                    var people = cursor.Current;
                    foreach (var doc in people)
                    {
                        Console.WriteLine(doc);
                    }
                }
            }
        }

        public List<SearchResult> getFromCache(SearchQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
