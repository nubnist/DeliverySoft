using DeliverySoft.DomainServiceEmployees.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace DeliverySoft.DomainServiceEmployees.DALService;

public static class EmployeeServiceExtensions
{
    public static IServiceCollection AddDomainServiceEmployees(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        return services;
    }
}