using System.Linq.Expressions;

namespace DeliverySoft.DomainService.CommonDTOs;

/// <summary>
/// Набор расширений для подключения требуемых к возвращению полей
/// </summary>
public static class IncludeEntitiesExtensions
{
    public static IIncludeEntity<TRequest, TEntity, TProperty> Include<TRequest, TEntity, TProperty>(this IIncludeEntity<TRequest, TEntity> source, Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class
    {
        var memberExpression = navigationPropertyPath.Body as MemberExpression;
        var item = new IncludeEntity<TRequest, TEntity, TProperty>
        {
            FromMainObject = true,
            Parent = source,
            MemberInfo = memberExpression.Member
        };
        return item;
    }

    public static IIncludeEntity<TRequest, TEntity, TProperty> ThenInclude<TRequest, TEntity, TPreviousProperty, TProperty>(this IIncludeEntity<TRequest, TEntity, TPreviousProperty> source, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class
    {
        var memberExpression = navigationPropertyPath.Body as MemberExpression;
        var item = new IncludeEntity<TRequest, TEntity, TProperty>
        {
            FromMainObject = false,
            Parent = source,
            MemberInfo = memberExpression.Member
        };
        return item;
    }

    public static IIncludeEntity<TRequest, TEntity, TProperty> ThenInclude<TRequest, TEntity, TPreviousProperty, TProperty>(this IIncludeEntity<TRequest, TEntity, TPreviousProperty[]> source, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class
    {
        var memberExpression = navigationPropertyPath.Body as MemberExpression;
        var item = new IncludeEntity<TRequest, TEntity, TProperty>
        {
            FromMainObject = false,
            Parent = source,
            MemberInfo = memberExpression.Member
        };
        return item;
    }

    public static RequestWithInclude<TRequest> ToRequest<TRequest>(this IIncludeEntity<TRequest> data) where TRequest : class
    {
        List<IncludeFields> members = new List<IncludeFields>();
        while (true)
        {
            switch (data)
            {
                case TRequest request:
                    members.Reverse();
                    return new RequestWithInclude<TRequest>
                    {
                        Request = request,
                        IncludeMembers = members
                    };
                case IncludeEntity<TRequest> includeEntity:
                    members.Add(new IncludeFields
                    {
                        FromMainObject = includeEntity.FromMainObject,
                        MemberName = includeEntity.MemberInfo.Name
                    });
                    data = includeEntity.Parent;
                    break;
            }
        }
    }
}