using DeliverySoft.Core;
using DeliverySoft.DomainService.ClientHelpers;
using DeliverySoft.DomainServiceClients.ApiClient.Api;
using DeliverySoft.DomainServiceClients.ApiClient.Interfaces;
using DeliverySoft.DomainServiceClients.Dto.Models;
using DeliverySoft.DomainServiceClients.Dto.Requests;
using DeliverySoft.DomainServiceClients.WebContracts;

namespace DeliverySoft.DomainServiceClients.ApiClient;

public class DomainServiceClientsClient : IDomainServiceClientsClient
{
    private IDomainServiceClientsClientApi Api { get; set; }

    public DomainServiceClientsClient(IDomainServiceClientsClientApi api)
    {
        this.Api = api;
    }
    
    public Task<Client[]> GetClients(ArrayFilter<int> ids = null, GetClientsRequest request = null, PaginationOptions pagination = default, CancellationToken cancellationToken = default)
        => Wrappers.SendRequest(() => this.Api.GetClients(new GetClientsWebContract()
        {
            Ids = ids,
            Request = request,
            Pagination = pagination
        }, cancellationToken));

    public Task<int> SaveClient(SaveClientRequest request)
        => Wrappers.SendRequest(() => this.Api.SaveClient(request));
}