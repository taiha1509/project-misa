using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBLMongo
{
    class Command
    {
        public string _connectionString;
        public string _databaseName;
        public string _collectionName;

        public Command(string connectionString, string databaseName, string collectionName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
            _collectionName = collectionName;
        }
        public void addCommand()
        {
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase(_databaseName);
            var collection = database.GetCollection<BsonDocument>(_collectionName);

            var json = "{created: {$gte: ISODate(\"2018-12-20T00:00:00.000Z\"), $lt: ISODate(\"2018-12-21T00:00:00.000Z\")}}";
            BsonDocument query = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);
            var documents = collection.Find(query).Limit(10);
            Console.WriteLine(documents.Any());
        }
    }
}
