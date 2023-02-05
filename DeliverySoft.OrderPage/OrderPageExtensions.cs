using Microsoft.Extensions.DependencyInjection;

namespace DeliverySoft.OrderPage;

public static class OrderPageExtensions
{
    public static IServiceCollection AddOrderPage(this IServiceCollection services)
    {
        services.AddScoped<OrderPage>();
        return services;
    }
}