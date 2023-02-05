using DeliverySoft.Core;
using DeliverySoft.DomainServiceClients.Dto.Requests;

namespace DeliverySoft.DomainServiceClients.WebContracts;

public class GetClientsWebContract
{
    public ArrayFilter<int> Ids { get; set; }
    public GetClientsRequest Request { get; set; }
    public PaginationOptions Pagination { get; set; }
}