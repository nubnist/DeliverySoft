using DeliverySoft.Core;
using DeliverySoft.DomainService.CommonDTOs;
using DeliverySoft.DomainServiceOrders.Dto.Requests;

namespace DeliverySoft.DomainServiceOrders.WebContracts;

public class GetOrdersWebContract
{
    public ArrayFilter<int> Ids { get; set; }
    public RequestWithInclude<GetOrdersRequest> Request { get; set; }
    public PaginationOptions Pagination { get; set; }
}