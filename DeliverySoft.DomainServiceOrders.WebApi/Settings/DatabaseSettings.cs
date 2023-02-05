namespace DeliverySoft.DomainServiceOrders.WebApi.Settings;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public bool LoggingEnabled { get; set; }
}