using DeliverySoft.Core.Paging;

namespace DeliverySoft.DomainServiceEmployees.Dto.Requests
{
    public class PaginationOptions : IPagingRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}