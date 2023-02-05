using DeliverySoft.Core;
using DeliverySoft.DomainServiceEmployees.Dto.Requests;

namespace DeliverySoft.DomainServiceEmployees.WebContracts;

public class GetEmployeesWebContract
{
    public ArrayFilter<int> Ids { get; set; }
    public GetEmployeesRequest Request { get; set; }
    public PaginationOptions Pagination { get; set; }
}