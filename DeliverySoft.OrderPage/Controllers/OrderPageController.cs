using DeliverySoft.Core.Paging;
using DeliverySoft.OrderPage.Models;
using DeliverySoft.OrderPage.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySoft.OrderPage.Controllers
{
    /// <summary>
    /// Страница заказов
    /// </summary>
    [ApiController, Route("api/[controller]")]
    public class OrderPageController : ControllerBase
    {
        private OrderPage OrderPage { get; }
        public OrderPageController(OrderPage orderPage)
        {
            this.OrderPage = orderPage;
        }

        /// <summary>
        /// Получить список клиентов
        /// </summary>
        [HttpGet("[action]")]
        public Task<ClientModel[]> GetClients(CancellationToken cancellationToken = default)
            => this.OrderPage.GetClients(cancellationToken);

        /// <summary>
        /// Получить список статусов
        /// </summary>
        [HttpGet("[action]")]
        public Task<OrderStatusModel[]> GetOrderStatuses(CancellationToken cancellationToken = default)
            => this.OrderPage.GetOrderStatuses(cancellationToken);
        
        /// <summary>
        /// Сохранить клиента
        /// </summary>
        [HttpPost("[action]")]
        public Task<int> SaveClient([FromBody] SaveClientRequest request)
            => this.OrderPage.SaveClient(request);

        /// <summary>
        /// Сохранить заказ
        /// </summary>
        [HttpPost("[action]")]
        public Task<int> SaveOrder(SaveOrderRequest request)
            => this.OrderPage.SaveOrder(request);

        /// <summary>
        /// Получить список заказов
        /// </summary>
        [HttpGet("[action]")]
        public Task<PagingResponse<OrderModel>> GetOrders([FromQuery] GetOrdersRequest request, CancellationToken cancellationToken = default)
            => this.OrderPage.GetOrders(request, cancellationToken);

        [HttpDelete("[action]")]
        public Task DeleteOrder([FromQuery] int id)
            => this.OrderPage.DeleteOrder(id);
    }
}
