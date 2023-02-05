namespace DeliverySoft.DomainService.CommonDTOs
{
    public interface IIncludeEntity<TRequest> { }

    public interface IIncludeEntity<TRequest, TEntity> : IIncludeEntity<TRequest> { }

    public interface IIncludeEntity<TRequest, TEntity, TProperty> : IIncludeEntity<TRequest, TEntity> { }
}
