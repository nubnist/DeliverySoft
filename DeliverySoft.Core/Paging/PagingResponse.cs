
namespace DeliverySoft.Core.Paging
{
    /// <summary>
    /// Пагинированный результат
    /// </summary>
    public class PagingResponse<TEntity>
    {
        public TEntity[] Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool IsFinalBlock { get; set; }
    }
}
