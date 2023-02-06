using System.Net;
using DeliverySoft.Core;
using DeliverySoft.DomainService.CommonDTOs;
using DeliverySoft.DomainService.Helpers;
using DeliverySoft.DomainServiceOrders.DALService.Helpers;
using DeliverySoft.DomainServiceOrders.DALService.Mapping;
using DeliverySoft.DomainServiceOrders.Dto;
using DeliverySoft.DomainServiceOrders.Dto.Models;
using DeliverySoft.DomainServiceOrders.Dto.Requests;
using Microsoft.EntityFrameworkCore;
using MovieList.DAL.Abstractions;
using Entities = DeliverySoft.DAL.Entities;

namespace DeliverySoft.DomainServiceOrders.DALService;

public class OrderService : IOrderService
{
    private ISiteDbContext SiteDbContext { get; }

    public OrderService(ISiteDbContext siteDbContext)
    {
        this.SiteDbContext = siteDbContext;
    }

    public async Task<Order[]> GetOrders(ArrayFilter<int> ids = null, 
                                         RequestWithInclude<GetOrdersRequest> request = null, 
                                         PaginationOptions pagination = null,
                                         CancellationToken cancellationToken = default)
    {
        IQueryable<Entities.Order> query = this.SiteDbContext.Orders;

        if (request != null)
        {
            query = query.InlcudeQuery(request.IncludeMembers);
        }

        if (ids != null)
        {
            query = ids.Inverted 
                ? query.Where(e => !ids.Values.Contains(e.Id)) 
                : query.Where(e => ids.Values.Contains(e.Id));
        }
        
        if (!string.IsNullOrWhiteSpace(request?.Request?.Search))
        {
            query = query.SearchStringOrderHelper(request.Request.Search);
        }

        query = query.PaginationQuery(pagination);

        var result = await query.ToArrayAsync(cancellationToken);
        return result.Select(MappingExtensions.Map).ToArray();
    }

    public async Task<int> SaveOrder(SaveOrderRequest request)
    {
        Entities.Order order;
        if (request.Id == 0)
        {
            order = new Entities.Order();
            this.SiteDbContext.Orders.Add(order);
        }
        else
        {
            order = await this.SiteDbContext.Orders
                .Include(o => o.AppointedEmployees)
                .FirstOrDefaultAsync(v => v.Id == request.Id);
            if (order == null)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, "Указанная заявка не найдена");
            }
        }

        if (request.Comment?.IsDefined == true) order.Comment = request.Comment.Value;
        if (request.DeliveryLocation?.IsDefined == true) order.DeliveryLocation = request.DeliveryLocation.Value;
        if (request.Title?.IsDefined == true) order.Title = request.Title.Value;
        if (request.DeliveryDate?.IsDefined == true) order.DeliveryDate = request.DeliveryDate.Value;
        if (request.StatusId?.IsDefined == true) order.StatusId = request.StatusId.Value;
        if (request.ClientId?.IsDefined == true) order.ClientId = request.ClientId.Value;

        if (request.EmployeesIds?.IsDefined == true)
        {
            this.SiteDbContext.AppointedEmployees.RemoveRange(order.AppointedEmployees); // Можно оптимизировать, но для простоты пусть будет так
            foreach (var employeeId in request.EmployeesIds.Value)
            {
                order.AppointedEmployees.Add(new Entities.AppointedEmployee()
                {
                    EmployeeId = employeeId
                });
            }
        }

        await this.SiteDbContext.SaveChangesAsync();

        return order.Id;
    }

    public async Task<OrderStatus[]> GetOrderStatuses(CancellationToken cancellationToken = default)
    {
        IQueryable<Entities.OrderStatus> query = this.SiteDbContext.OrderStatuses;

        var result = await query.ToArrayAsync(cancellationToken);
        return result.Select(MappingExtensions.Map).ToArray();
    }

    public async Task DeleteOrder(int id)
    {
        var order = await this.SiteDbContext.Orders
            .Include(o => o.AppointedEmployees)
            .FirstOrDefaultAsync(v => v.Id == id);

        this.SiteDbContext.Orders.Remove(order);
        await this.SiteDbContext.SaveChangesAsync();
    }
}