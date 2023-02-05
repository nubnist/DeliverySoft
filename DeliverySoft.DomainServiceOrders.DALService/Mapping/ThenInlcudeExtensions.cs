using DeliverySoft.DomainService.CommonDTOs;
using Microsoft.EntityFrameworkCore.Query;
using Entities = DeliverySoft.DAL.Entities;

namespace DeliverySoft.DomainServiceOrders.DALService.Mapping
{
    public static class ThenInlcudeExtensions
    {
        private delegate IQueryable<TEntity> IterationMethod<TEntity, TProperty>(string name, IIncludableQueryable<TEntity, TProperty> query, ref int index) where TEntity : class;
        private static IQueryable<TEntity> BaseThenInlcudeQuery<TEntity, TProperty>(this IIncludableQueryable<TEntity, TProperty> query,
                                                                                    List<IncludeFields> includeMembers,
                                                                                    ref int index,
                                                                                    IterationMethod<TEntity, TProperty> getData) where TEntity : class
        {
            int nextIndex = index + 1;
            if (includeMembers.Count <= nextIndex || includeMembers[nextIndex].FromMainObject)
            {
                return query;
            }
            index++;
            return getData(includeMembers[index].MemberName, query, ref index);
        }

        public static IQueryable<TEntity> ThenInlcudeQuery<TEntity>(this IIncludableQueryable<TEntity, Entities.OrderStatus> query, List<IncludeFields> includeMembers, ref int index) where TEntity : class
           => query.BaseThenInlcudeQuery(includeMembers, ref index, (string memberName, IIncludableQueryable<TEntity, Entities.OrderStatus> query, ref int index)
               => memberName switch
               {
                   _ => throw new NotImplementedException(),
               });
    }
}
