using DeliverySoft.Core;
using DeliverySoft.DomainServiceEmployees.Dto.Models;
using DeliverySoft.DomainServiceEmployees.Dto.Requests;
using DeliverySoft.DomainServiceEmployees.WebContracts;
using Refit;

namespace DeliverySoft.DomainServiceEmployees.ApiClient.Api;

public interface IDomainServiceEmployeesClientApi
{
    [Post("/GetEmployees")]
    Task<ApiResponse<Employee[]>> GetEmployees([Body] GetEmployeesWebContract request, CancellationToken cancellationToken);

    [Post("/SaveEmployee")]
    Task<ApiResponse<int>> SaveEmployee([Body] SaveEmployeeRequest request);
}