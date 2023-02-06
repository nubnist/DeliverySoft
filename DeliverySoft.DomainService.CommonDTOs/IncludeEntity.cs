using System.Reflection;

namespace DeliverySoft.DomainService.CommonDTOs;

class IncludeEntity<TRequest> : IIncludeEntity<TRequest>
{
    public bool FromMainObject { get; set; }
    public IIncludeEntity<TRequest> Parent { get; set; }
    public MemberInfo MemberInfo { get; set; }
}

class IncludeEntity<TRequest, TEntity, TProperty> : IncludeEntity<TRequest>, IIncludeEntity<TRequest, TEntity, TProperty> { }