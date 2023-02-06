using DeliverySoft.DomainServiceOrders.Dto.Models;
using DeliverySoft.DomainServiceOrders.Dto.Requests;
using DeliverySoft.DomainServiceOrders.WebContracts;
using Refit;

namespace DeliverySoft.DomainServiceOrders.ApiClient.Api;

public interface IDomainServiceOrdersClientApi
{
    [Post("/GetOrders")]
    Task<ApiResponse<Order[]>> GetOrders([Body] GetOrdersWebContract request, CancellationToken cancellationToken);
    
    [Post("/SaveOrder")]
    Task<ApiResponse<int>> SaveOrder([Body] SaveOrderRequest request);
    
    [Get("/GetOrderStatuses")]
    Task<ApiResponse<OrderStatus[]>> GetOrderStatuses(CancellationToken cancellationToken);

    [Delete("/DeleteOrder/{id}")]
    Task DeleteOrder(int id);
}