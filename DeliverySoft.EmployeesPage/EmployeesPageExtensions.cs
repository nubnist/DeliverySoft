using Microsoft.Extensions.DependencyInjection;

namespace DeliverySoft.EmployeesPage;

public static class EmployeesPageExtensions
{
    public static IServiceCollection AddEmployeesPage(this IServiceCollection services)
    {
        services.AddScoped<EmployeesPage>();
        return services;
    }
}