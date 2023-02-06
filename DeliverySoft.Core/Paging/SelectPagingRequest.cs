namespace DeliverySoft.Core.Paging
{
    public class SelectPagingRequest : IPagingRequest
    {
        public string? Search { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
