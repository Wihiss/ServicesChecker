using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ServicesCheckerLib.Def;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Impl.MongoDb
{
    internal class OutputElement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ServiceName { get; set; }

        public DateTime Timestamp { get; set; }

        // [JsonConverter(typeof(StringEnumConverter))]  // JSON.Net
        // [BsonRepresentation(BsonType.String)]         // Mongo
        public ServiceStatus Status { get; set; }

        public string LastError { get; set; }
    }
}
