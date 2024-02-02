using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ski_ServiceNoSQL.Models
{
    public class Mitarbeiter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public int counter { get; set; }
    }
}
