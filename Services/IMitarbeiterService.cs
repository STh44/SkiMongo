using Microsoft.AspNetCore.Mvc;
using Ski_ServiceNoSQL.Models;

namespace Ski_ServiceNoSQL.Services
{
    public interface IMitarbeiterService
    {
        public JsonResult? ProveUser(Mitarbeiter mitarbeiter);
        public List<Mitarbeiter> AllMitarbeiter();
        public Mitarbeiter Deblocker(string id);
    }
}
