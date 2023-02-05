using DeliverySoft.DomainService.ClientHelpers;
using DeliverySoft.DomainServiceOrders.ApiClient.Api;
using DeliverySoft.DomainServiceOrders.ApiClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace DeliverySoft.DomainServiceOrders.ApiClient;

public static class DomainServiceOrdersClientExtensions
{
    public static IServiceCollection AddDomainServiceOrdersClient(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IDomainServiceOrdersClient, DomainServiceOrdersClient>();
        services.AddRefitClient<IDomainServiceOrdersClientApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(Wrappers.GetUrl(new[] { connectionString, "api/OrderService" }));
            });
        
        return services;
    }
}