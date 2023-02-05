using DeliverySoft.OrderPage.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySoft.OrderPage.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class OrderPageController : ControllerBase
    {
        private OrderPage OrderPage { get; }
        public OrderPageController(OrderPage orderPage)
        {
            this.OrderPage = orderPage;
        }

        [HttpPost("[action]")]
        public Task<int> SaveClient([FromBody] SaveClientRequest request, CancellationToken cancellationToken = default)
            => this.OrderPage.SaveClient(request, cancellationToken);
    }
}
