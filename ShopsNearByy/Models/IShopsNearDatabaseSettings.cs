namespace ShopsNearByy.Models
{
    public interface IShopsNearDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
