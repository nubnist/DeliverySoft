using DeliverySoft.DomainServiceClients.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace DeliverySoft.DomainServiceClients.DALService;

public static class ClientServiceExtensions
{
    public static IServiceCollection AddDomainServiceClient(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        return services;
    }
}