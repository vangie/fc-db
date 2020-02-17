using System;
using System.IO;
using System.Text;
using Aliyun.Serverless.Core;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mongodb
{
    public class MongoDBHandler
    {
        private static String dbName = Environment.GetEnvironmentVariable("MONGO_DATABASE");
        private static String connectionUrl = Environment.GetEnvironmentVariable("MONGO_URL");
        public Stream Handler(Stream input, IFcContext context)
        {
            var client = new MongoClient(connectionUrl);
            var database = client.GetDatabase(dbName);
            var collection = database.GetCollection<BsonDocument>("fc_col");
            var document = new BsonDocument
            {
                { "DEMO", "FC" },
                { "MSG", "Hello FunctionCompute For MongoDB" }
            };
            collection.InsertOne(document);
            var doc = collection.Find(new BsonDocument{{ "DEMO", "FC"}}).FirstOrDefault();
            Console.WriteLine(doc.ToString());

            byte[] docBytes = Encoding.UTF8.GetBytes(doc.ToString());
            MemoryStream output = new MemoryStream();
            output.Write(docBytes, 0, docBytes.Length);
            return output;
        }

    }
}
