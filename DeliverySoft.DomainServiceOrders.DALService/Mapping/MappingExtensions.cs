using DeliverySoft.DomainServiceOrders.Dto.Models;
using Entities = DeliverySoft.DAL.Entities;

namespace DeliverySoft.DomainServiceOrders.DALService.Mapping;

public static class MappingExtensions
{
    public static Order Map(Entities.Order order)
    {
        if (order == null) return null;
        var result = new Order();
        result.Id = order.Id;
        result.DeliveryLocation = order.DeliveryLocation;
        result.Title = order.Title;
        result.DeliveryDate = order.DeliveryDate;
        result.Comment = order.Comment;
        result.ClientId = order.ClientId;
        
        result.StatusId = order.StatusId;
        result.Status = Map(order.Status);

        if (order.AppointedEmployees != null)
        {
            result.EmployeesIds = order.AppointedEmployees
                .Select(e => e.EmployeeId)
                .ToArray();
        }
        
        return result;
    }
    
    public static OrderStatus Map(Entities.OrderStatus status)
    {
        if (status == null) return null;
        var result = new OrderStatus();
        result.Id = status.Id;
        result.Title = status.Title;
        result.RequireComment = status.RequireComment;
        result.AllowedChangeOrderData = status.AllowedChangeOrderData;

        return result;
    }
}