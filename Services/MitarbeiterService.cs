using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Ski_ServiceNoSQL.Models;

namespace Ski_ServiceNoSQL.Services
{
    public class MitarbeiterService : IMitarbeiterService
    {
        private readonly IMongoCollection<Mitarbeiter> _orders;
        public ITokenService _tokenService;

        public MitarbeiterService(ISkiDatabaseSettings settings, ITokenService tokenService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Mitarbeiter>(settings.MitarbeiterCollectionName);
            _tokenService = tokenService;
        }

        public List<Mitarbeiter> AllMitarbeiter() =>
            _orders.Find(order => true).ToList();

        /// <summary>
        /// Mitarbeiter Autorisation,
        /// </summary>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        /// 
        public int counter;
        public List<Mitarbeiter> mitarbeiters;
        public JsonResult? ProveUser(Mitarbeiter mitarbeiter)
        {
            mitarbeiters = _orders.Find(order => true).ToList();
            foreach (var m in mitarbeiters)
            {
                if (m.name == mitarbeiter.name && m.password == mitarbeiter.password)
                {
                    return new JsonResult(new { userName = mitarbeiter.name, token = _tokenService.CreateToken(mitarbeiter.name) });
                }
                else if (m.name == mitarbeiter.name && m.password != mitarbeiter.password)
                {
                    m.counter += 1;
                    _orders.ReplaceOne(order => order.name == m.name, m);
                    if (m.counter >= 3)
                    {
                        return new JsonResult(new { gespert = m.counter });
                    }
                }
            }
            return new JsonResult(new { falsch = "Falsch" });
        }

        /// <summary>
        /// Mitarbeiter Freigeben 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Mitarbeiter Deblocker(string id)
        {
            Mitarbeiter? mitarbeiter = _orders.Find(order => order.Id == id).FirstOrDefault();
            if (mitarbeiter != null)
            {
                mitarbeiter.counter = 0;
                _orders.ReplaceOne(order => order.Id == id, mitarbeiter);
                return mitarbeiter;
            }
            return null;
        }
    }
}
