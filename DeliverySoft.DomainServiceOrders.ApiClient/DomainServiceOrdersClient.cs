using DeliverySoft.Core;
using DeliverySoft.DomainService.ClientHelpers;
using DeliverySoft.DomainService.CommonDTOs;
using DeliverySoft.DomainServiceOrders.ApiClient.Api;
using DeliverySoft.DomainServiceOrders.ApiClient.Interfaces;
using DeliverySoft.DomainServiceOrders.Dto.Models;
using DeliverySoft.DomainServiceOrders.Dto.Requests;
using DeliverySoft.DomainServiceOrders.WebContracts;

namespace DeliverySoft.DomainServiceOrders.ApiClient;

public class DomainServiceOrdersClient : IDomainServiceOrdersClient
{
    private IDomainServiceOrdersClientApi Api { get; set; }

    public DomainServiceOrdersClient(IDomainServiceOrdersClientApi api)
    {
        this.Api = api;
    }
    
    public Task<Order[]> GetOrders(ArrayFilter<int> ids = null, RequestWithInclude<GetOrdersRequest> request = null, PaginationOptions pagination = null, CancellationToken cancellationToken = default)
        => Wrappers.SendRequest(() => this.Api.GetOrders(new GetOrdersWebContract()
            {
                Ids = ids,
                Request = request,
                Pagination = pagination
            }, cancellationToken));

    public Task<int> SaveOrder(SaveOrderRequest request)
        => Wrappers.SendRequest(() => this.Api.SaveOrder(request));

    public Task<OrderStatus[]> GetOrderStatuses(CancellationToken cancellationToken = default)
        => Wrappers.SendRequest(() => this.Api.GetOrderStatuses(cancellationToken));

    public Task DeleteOrder(int id)
        => Wrappers.SendRequest(() => this.Api.DeleteOrder(id));
}