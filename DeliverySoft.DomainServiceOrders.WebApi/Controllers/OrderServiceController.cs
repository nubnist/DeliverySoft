using DeliverySoft.DomainServiceOrders.Dto;
using DeliverySoft.DomainServiceOrders.Dto.Models;
using DeliverySoft.DomainServiceOrders.Dto.Requests;
using DeliverySoft.DomainServiceOrders.WebContracts;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySoft.DomainServiceOrders.WebApi.Controllers;

[ApiController, Route("api/[controller]")]
public class OrderServiceController : ControllerBase
{
    public IOrderService OrderService { get; set; }
    public OrderServiceController(IOrderService employeeService)
    {
        this.OrderService = employeeService;
    }
    
    [HttpPost("GetOrders")]
    public Task<Order[]> GetOrders([FromBody] GetOrdersWebContract request, CancellationToken cancellationToken)
        => this.OrderService.GetOrders(request.Ids, request.Request, request.Pagination, cancellationToken);
    
    [HttpPost("SaveOrder")]
    public Task<int> SaveOrder([FromBody] SaveOrderRequest request)
        => this.OrderService.SaveOrder(request);
    
    [HttpGet("GetOrderStatuses")]
    public Task<OrderStatus[]> GetOrderStatuses(CancellationToken cancellationToken)
        => this.OrderService.GetOrderStatuses(cancellationToken);
}