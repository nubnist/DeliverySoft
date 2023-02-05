using DeliverySoft.DomainService.ClientHelpers;
using DeliverySoft.DomainServiceClients.ApiClient.Api;
using DeliverySoft.DomainServiceClients.ApiClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace DeliverySoft.DomainServiceClients.ApiClient;

public static class DomainServiceClientsClientExtensions
{
    public static IServiceCollection AddDomainServiceClientsClient(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IDomainServiceClientsClient, DomainServiceClientsClient>();
        services.AddRefitClient<IDomainServiceClientsClientApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(Wrappers.GetUrl(new[] { connectionString, "api/ClientService" }));
            });
        
        return services;
    }
}