using DeliverySoft.DomainServiceOrders.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace DeliverySoft.DomainServiceOrders.DALService;

public static class OrderServiceExtensions
{
    public static IServiceCollection AddDomainServiceEmployees(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        return services;
    }
}