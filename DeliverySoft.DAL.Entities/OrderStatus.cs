namespace DeliverySoft.DAL.Entities;

public class OrderStatus
{
    public int Id { get; set; }
    public string Title { get; set; }

    public bool AllowedChangeOrderData { get; set; }
    public bool RequireComment  { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}