using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Ski_ServiceNoSQL.Models;

namespace Ski_ServiceNoSQL.Services
{
    public class OrdersService : IOrdersService
    {

        private readonly IMongoCollection<Orders> _orders;

        public OrdersService(ISkiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Orders>(settings.OrdersCollectionName);
        }

        public List<Orders> Get() =>
            _orders.Find(order => true).ToList();

        public async Task<List<Orders>> GetPriority()
        {
            var sort = Builders<Orders>.Sort.Ascending(o => o.Priorität); ;
            var results = await _orders.Find(new BsonDocument()).Sort(sort).ToListAsync();
            return results;
        }

        public Orders Get(string id) =>
            _orders.Find(o => o.Id == id).FirstOrDefault();

        public Orders Create(Orders order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public void Update(string id, Orders orderIn) =>
            _orders.ReplaceOne(order => order.Id == id, orderIn);
        
        public void Remove(Orders orderIn) =>
            _orders.DeleteOne(order => order.Id == orderIn.Id);

        public void Remove(string id) =>
            _orders.DeleteOne(order => order.Id == id);
    }
}
