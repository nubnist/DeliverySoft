using DeliverySoft.DomainService.CommonDTOs;
using DeliverySoft.DomainServiceOrders.Dto.Models;
using Microsoft.EntityFrameworkCore;
using Entities = DeliverySoft.DAL.Entities;

namespace DeliverySoft.DomainServiceOrders.DALService.Mapping
{
    public static class InlcudeExtensions
    {
        private delegate IQueryable<TEntity> IterationMethod<TEntity>(string name, IQueryable<TEntity> query, ref int index);
        private static IQueryable<TEntity> BaseInlcudeQuery<TEntity>(this IQueryable<TEntity> query,
                                                                     List<IncludeFields> includeMembers,
                                                                     IterationMethod<TEntity> getData)
        {
            for (int i = 0; i < includeMembers.Count; i++)
            {
                if (includeMembers[i].FromMainObject)
                {
                    query = getData(includeMembers[i].MemberName, query, ref i);
                }
            }
            return query;
        }

        public static IQueryable<Entities.Order> InlcudeQuery(this IQueryable<Entities.Order> query, List<IncludeFields> includeMembers)
           => query.BaseInlcudeQuery(includeMembers, (string memberName, IQueryable<Entities.Order> query, ref int index)
               => memberName switch
               {
                   nameof(Order.Status) => query.Include(p => p.Status).ThenInlcudeQuery(includeMembers, ref index),
                   nameof(Order.EmployeesIds) => query.Include(p => p.AppointedEmployees),
                   _ => throw new NotImplementedException(),
               });
    }
}
