using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoConnection
{
    class Infos
    {
        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        [BsonRequired()]
        public string Name { get; set; }

        [BsonElement("surname")]
        [BsonRequired()]
        public string Surname { get; set; }

        [BsonElement("date")]
        [BsonRequired()]
        public DateTime Date { get; set; }

        [BsonElement("height")]
        [BsonRequired()]
        public double Height { get; set; }

        [BsonElement("age")]
        [BsonRequired()]
        public double Age { get; set; }
    }
}
