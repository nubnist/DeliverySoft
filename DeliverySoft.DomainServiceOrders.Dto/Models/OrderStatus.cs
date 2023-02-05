namespace DeliverySoft.DomainServiceOrders.Dto.Models;

public class OrderStatus
{
    public int Id { get; set; }
    public string Title { get; set; }

    public bool AllowedChangeOrderData { get; set; }
    public bool RequireComment  { get; set; }
}