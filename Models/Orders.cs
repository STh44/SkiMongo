using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ski_ServiceNoSQL.Models
{
    public class Orders
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }        
        public string? Kundenname { get; set; }       
        public string? Email { get; set; }       
        public string? Telefon { get; set; }
        public string? Priorität { get; set; }
        public string? Dienstleistung { get; set; }
        public string? Status { get; set; }
        public DateTime? Erfassungsdatum { get; set; }
        public DateTime? Abholdatum { get; set; }
    }
}
