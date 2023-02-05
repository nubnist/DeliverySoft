using DeliverySoft.DomainService.ClientHelpers;
using DeliverySoft.DomainServiceEmployees.ApiClient.Api;
using DeliverySoft.DomainServiceEmployees.ApiClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace DeliverySoft.DomainServiceEmployees.ApiClient;

public static class DomainServiceEmployeesClientExtensions
{
    public static IServiceCollection AddDomainServiceEmployeesClient(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IDomainServiceEmployeesClient, DomainServiceEmployeesClient>();
        var refitClient = services.AddRefitClient<IDomainServiceEmployeesClientApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(Wrappers.GetUrl(new[] { connectionString, "api/EmployeeService" }));
            });
        
        return services;
    }
}