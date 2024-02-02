
using Ski_ServiceNoSQL.Models;

namespace Ski_ServiceNoSQL.Services
{
    public interface IOrdersService
    {
        List<Orders> Get();
        Task<List<Orders>> GetPriority();
        Orders Get(string id);
        Orders Create(Orders order);
        void Update(string id, Orders orderIn);
        void Remove(Orders orderIn);
        void Remove(string id);
    }
}
