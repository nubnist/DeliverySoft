namespace DeliverySoft.DomainService.CommonDTOs;

public class RequestWithInclude<TEntity> where TEntity : class
{
    public TEntity Request { get; set; }
    public List<IncludeFields> IncludeMembers { get; set; } = new List<IncludeFields>();
}