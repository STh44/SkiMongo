namespace Ski_ServiceNoSQL.Models
{
    public interface ISkiDatabaseSettings
    {
        string OrdersCollectionName { get; set; }
        string MitarbeiterCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
