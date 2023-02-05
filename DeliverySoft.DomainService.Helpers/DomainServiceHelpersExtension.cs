using DeliverySoft.Core.Paging;

namespace DeliverySoft.DomainService.Helpers
{
    public static class DomainServiceHelpersExtension
    {
        public static IQueryable<TEntity> PaginationQuery<TEntity>(this IQueryable<TEntity> query, IPagingRequest pagination)
        {
            if (pagination?.PageSize > 0)
            {
                query = query.Skip(pagination.PageNumber * pagination.PageSize)
                             .Take(pagination.PageSize);
            }
            return query;
        }
    }
}
