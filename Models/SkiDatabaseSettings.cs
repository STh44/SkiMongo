namespace Ski_ServiceNoSQL.Models
{
    public class SkiDatabaseSettings : ISkiDatabaseSettings
    {
        public string OrdersCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string MitarbeiterCollectionName { get; set; } = string.Empty;
    }
    
}
