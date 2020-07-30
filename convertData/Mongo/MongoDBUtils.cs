using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoDB.Libmongocrypt;
using System;
using System.Collections.Generic;
using System.Text;

namespace convertData.Mongo
{
    class MongoDBUtils
    {

        public MongoClient dbClient;

        public MongoDBUtils(string connection)
        {
            dbClient = new MongoClient(connection);
        }

        [Obsolete]
        public void insertData(string DbName, String CollectionName, Model model)
        {
            var database = dbClient.GetDatabase(DbName);
            var collection = database.GetCollection<BsonDocument>(CollectionName);

            

            var document = new BsonDocument
            {
                {"ID",model.ID},
                {"IPFrom", new BsonBinaryData(model.IPFrom, BsonBinarySubType.Binary) } ,
                {"IPTo", new BsonBinaryData(model.IPTo, BsonBinarySubType.OldBinary)},
                {"IPType",model.IPType},
                {"Country",model.Country},
                {"StateProvince", model.StateProvince },
                {"CityDistrict", model.CityDistrict },
                {"Latitude", model.Latitude },
                {"Longtitude", model.Longtitude },
                {"TimeZoneOffset", model.TimeZoneOffset },
                {"TimeZoneName", model.TimeZoneName },
                {"ISPName", model.ISPName },
                {"ConectionType", model.ConnectionType },
                {"OUName", model.OUName }
            };
            Console.WriteLine("IPFrom : " + new BsonBinaryData(model.IPFrom));
            Console.WriteLine("IPTo : " + new BsonBinaryData(model.IPTo));
            collection.InsertOne(document);

       
        }

        public void closeConnection()
        {
        }

    }
}
