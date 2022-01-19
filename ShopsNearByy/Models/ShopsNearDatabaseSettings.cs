namespace ShopsNearByy.Models
{
    public class ShopsNearDatabaseSettings : IShopsNearDatabaseSettings
    {
        public string UserCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
