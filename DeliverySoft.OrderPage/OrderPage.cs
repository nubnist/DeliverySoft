using System.Net;
using DeliverySoft.Core;
using DeliverySoft.Core.Paging;
using DeliverySoft.DomainService.CommonDTOs;
using DeliverySoft.DomainServiceClients.ApiClient.Interfaces;
using DeliverySoft.DomainServiceEmployees.ApiClient.Interfaces;
using DeliverySoft.DomainServiceOrders.ApiClient.Interfaces;
using DeliverySoft.DomainServiceOrders.Dto.Requests;
using DeliverySoft.OrderPage.Models;
using DeliverySoft.OrderPage.Requests;
using ClientsDto = DeliverySoft.DomainServiceClients.Dto;
using GetOrdersRequest = DeliverySoft.OrderPage.Requests.GetOrdersRequest;
using OrdersDto = DeliverySoft.DomainServiceOrders.Dto;
using SaveOrderRequest = DeliverySoft.OrderPage.Requests.SaveOrderRequest;

namespace DeliverySoft.OrderPage;

public class OrderPage
{
    private IDomainServiceClientsClient ClientsClient { get; }
    private IDomainServiceOrdersClient OrdersClient { get; }
    private IDomainServiceEmployeesClient EmployeesClient { get; }

    public OrderPage(IDomainServiceClientsClient clientsClient, 
                     IDomainServiceOrdersClient ordersClient, 
                     IDomainServiceEmployeesClient employeesClient)
    {
        this.ClientsClient = clientsClient;
        this.OrdersClient = ordersClient;
        this.EmployeesClient = employeesClient;
    }

    public async Task<int> SaveClient(SaveClientRequest request)
    {
        var saveRequest = new ClientsDto.Requests.SaveClientRequest();
        if (request.Id != 0)
        {
            _ = await this.ClientsClient.GetClients(ids: new ArrayFilter<int>(false, new[] { request.Id }))
                .ContinueWith(t => t.Result.FirstOrDefault()) ?? throw new ApiException(HttpStatusCode.BadRequest, "Указанный клиент не существует");
        }

        saveRequest.Id = request.Id;
        saveRequest.Name = request.Name;

        var clientId = await this.ClientsClient.SaveClient(saveRequest);

        return clientId;
    }

    public async Task<ClientModel[]> GetClients(CancellationToken cancellationToken)
    {
        var clients = await this.ClientsClient.GetClients(cancellationToken: cancellationToken);

        return clients.Select(c => new ClientModel()
        {
            Id = c.Id,
            Name = c.Name
        }).ToArray();
    }

    public async Task<PagingResponse<OrderModel>> GetOrders(GetOrdersRequest request, CancellationToken cancellationToken)
    {
        var orders = await this.OrdersClient.GetOrders(
            request: new OrdersDto.Requests.GetOrdersRequest()
            {
                Search = request.Search
            }.Include(o => o.EmployeesIds)
             .Include(o => o.Status)
             .ToRequest(),
            pagination: new PaginationOptions()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            }, cancellationToken: cancellationToken);

        var clients = await this.ClientsClient.GetClients(ids: new ArrayFilter<int>(false, orders.Select(o => o.ClientId).ToArray()), 
                                                          cancellationToken: cancellationToken);
        
        var employees = await this.EmployeesClient.GetEmployees(ids: new ArrayFilter<int>(false, orders.SelectMany(o => o.EmployeesIds).ToArray()), 
                                                                cancellationToken: cancellationToken);

        var items = orders.Select(o =>
        {
            bool isAllowedChange = o.Status.AllowedChangeOrderData;
            bool needCommentOnDelete = o.Status.RequireComment;
            var client = clients.First(c => c.Id == o.ClientId);

            var result = new OrderModel()
            {
                Id = o.Id,
                Title = o.Title,
                Client = new ClientModel()
                {
                    Id = client.Id,
                    Name = client.Name
                },
                Comment = o.Comment,
                DeliveryDate = o.DeliveryDate,
                DeliveryLocation = o.DeliveryLocation,
                IsAllowedChange = isAllowedChange,
                NeedCommentOnDelete = needCommentOnDelete,
                Status = new OrderStatusModel()
                {
                    Id = o.Status.Id,
                    Title = o.Status.Title,
                },
                Employees = employees.Where(e => o.EmployeesIds.Contains(e.Id)).Select(e => new EmployeeModel()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    MiddleName = e.MiddleName
                }).ToArray()
            };

            return result;
        }).ToArray();

        return new PagingResponse<OrderModel>()
        {
            PageSize = request.PageSize,
            PageNumber = request.PageNumber,
            Items = items,
            IsFinalBlock = items.Length != request.PageSize
        };
    }

    public async Task<int> SaveOrder(SaveOrderRequest request)
    {
        var saveRequest = new OrdersDto.Requests.SaveOrderRequest();

        var order = await this.OrdersClient.GetOrders(ids: new ArrayFilter<int>(false, new[] { request.Id }), 
                                                      request: new OrdersDto.Requests.GetOrdersRequest().Include(o => o.Status)
                                                                                                        .Include(o => o.EmployeesIds)
                                                                                                        .ToRequest())
            .ContinueWith(t => t.Result.FirstOrDefault());

        if (request.Id != 0 && order == null)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Вы пытаетесь сохранить несуществующий заказ");
        }

        var orderStatuses = await this.OrdersClient.GetOrderStatuses();

        var allowEditData = order?.Status.AllowedChangeOrderData ?? true; // Доступность редактирования данных
        var requireAddComment = orderStatuses.Any(s => s.RequireComment && s.Id == request.StatusId); // Необходимо ли устанавливать комментарий

        if (!allowEditData && IsOrderChanged(request, order))
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Запрещено менять данные заказа");
        }

        if (requireAddComment && string.IsNullOrWhiteSpace(request.Comment))
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Нужно обязательно добавить комментарий");
        }
        
        // ... Дополнительные валидации ...

        saveRequest.Id = request.Id;
        saveRequest.DeliveryLocation = request.DeliveryLocation;
        saveRequest.Title = request.Title;
        saveRequest.DeliveryDate = request.DeliveryDate;
        saveRequest.Comment = request.Comment;
        saveRequest.StatusId = request.StatusId;
        saveRequest.ClientId = request.ClientId;
        saveRequest.EmployeesIds = request.EmployeesIds;

        var clientId = await this.OrdersClient.SaveOrder(saveRequest);

        return clientId;
    }
    
    public async Task<OrderStatusModel[]> GetOrderStatuses(CancellationToken cancellationToken)
    {
        var statuses = await this.OrdersClient.GetOrderStatuses(cancellationToken: cancellationToken);

        return statuses.Select(c => new OrderStatusModel()
        {
            Id = c.Id,
            Title = c.Title
        }).ToArray();
    }

    public async Task DeleteOrder(int id)
    {
        var order = await this.OrdersClient.GetOrders(ids: new ArrayFilter<int>(false, new[] { id }))
            .ContinueWith(t => t.Result.FirstOrDefault()) ?? throw new ApiException(HttpStatusCode.BadRequest, "Указанный заказ не найден");

        await this.OrdersClient.DeleteOrder(id);
    }
    
    /// <summary>
    /// Проверка на измененные данные в заказе
    /// </summary>
    private bool IsOrderChanged(SaveOrderRequest request, OrdersDto.Models.Order order) 
        => order == null
           || request.ClientId != order.ClientId
           || request.EmployeesIds.Length != order.EmployeesIds.Length 
           || !request.EmployeesIds.All(ei => order.EmployeesIds.Contains(ei))
           || request.Title != order.Title
           || request.DeliveryDate != order.DeliveryDate
           || request.DeliveryLocation != order.DeliveryLocation;
}