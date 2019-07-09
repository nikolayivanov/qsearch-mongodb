using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace QSearch.Core.Impl
{
    public class MongoDbBasedCacheServiceAsync : ICacheServiceAsync
    {
        private const string colnameSearhResults = "searchresults";
        string connectionstr, databaseName;

        public MongoDbBasedCacheServiceAsync()
        {
            this.connectionstr = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;

            //take database name from connection string
            this.databaseName = MongoUrl.Create(this.connectionstr).DatabaseName;
        }

        public async Task addToCache(SearchQuery query, List<SearchResult> result)
        {
            var client = new MongoClient(this.connectionstr);
            var db = client.GetDatabase(this.databaseName);
            var collection = db.GetCollection<BsonDocument>(colnameSearhResults);            
            var doc = new BsonDocument () { 
                { "query", query.QueryText.ToLower() },
                { "result", GetBsonDocumentArray(result) }
            };

            await collection.InsertOneAsync(doc);
        }

        public async Task<List<SearchResult>> getFromCache(SearchQuery query)
        {
            var client = new MongoClient(this.connectionstr);
            var db = client.GetDatabase(this.databaseName);

            var collection = db.GetCollection<BsonDocument>(colnameSearhResults);
            var filter1 = new BsonDocument { { "query", query.QueryText.ToLower() } };
            var filter2 = Builders<BsonDocument>.Filter.Eq("query", query.QueryText.ToUpper());
            var filter = Builders<BsonDocument>.Filter.Or(new List<FilterDefinition<BsonDocument>> { filter1, filter2 });
            var results = await collection.Find(filter).ToListAsync();

            var reslist = new List<SearchResult>();
            foreach (var res in results)
            {
                foreach (var item in (BsonArray)res["result"])
                {
                    reslist.Add(new SearchResult()
                    {
                        link = item["link"].AsString,
                        title = item["title"].AsString
                    });
                }
            }

            return reslist;
        }

        private BsonArray GetBsonDocumentArray(IEnumerable<SearchResult> list)
        {
            var array = new BsonArray();
            foreach (var item in list)
            {
                array.Add(item.ToBsonDocument());
            }

            return array;
        }
    }
}
