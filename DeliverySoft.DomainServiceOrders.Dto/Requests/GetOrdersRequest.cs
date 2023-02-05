using DeliverySoft.DomainService.CommonDTOs;
using DeliverySoft.DomainServiceOrders.Dto.Models;

namespace DeliverySoft.DomainServiceOrders.Dto.Requests;

public class GetOrdersRequest  : IIncludeEntity<GetOrdersRequest, Order>
{
    public string Search { get; set; }
}