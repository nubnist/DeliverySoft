using DeliverySoft.Core;
using DeliverySoft.DomainService.CommonDTOs;
using DeliverySoft.DomainServiceOrders.Dto.Models;
using DeliverySoft.DomainServiceOrders.Dto.Requests;

namespace DeliverySoft.DomainServiceOrders.Dto;

public interface IOrderService
{
    Task<Order[]> GetOrders(ArrayFilter<int> ids = null, RequestWithInclude<GetOrdersRequest> request = null, PaginationOptions pagination = default, CancellationToken cancellationToken = default);
    Task<int> SaveOrder(SaveOrderRequest request);
    Task<OrderStatus[]> GetOrderStatuses(CancellationToken cancellationToken = default);
    Task DeleteOrder(int id);
}