using DeliverySoft.Core;
using DeliverySoft.DomainService.ClientHelpers;
using DeliverySoft.DomainServiceEmployees.ApiClient.Api;
using DeliverySoft.DomainServiceEmployees.ApiClient.Interfaces;
using DeliverySoft.DomainServiceEmployees.Dto.Models;
using DeliverySoft.DomainServiceEmployees.Dto.Requests;
using DeliverySoft.DomainServiceEmployees.WebContracts;

namespace DeliverySoft.DomainServiceEmployees.ApiClient;

public class DomainServiceEmployeesClient : IDomainServiceEmployeesClient
{
    private IDomainServiceEmployeesClientApi Api { get; set; }

    public DomainServiceEmployeesClient(IDomainServiceEmployeesClientApi api)
    {
        this.Api = api;
    }

    public Task<Employee[]> GetEmployees(ArrayFilter<int> ids = null, GetEmployeesRequest request = null, CancellationToken cancellationToken = default)
        => Wrappers.SendRequest(() => this.Api.GetEmployees(new GetEmployeesWebContract()
        {
            Ids = ids,
            Request = request
        }, cancellationToken));
}