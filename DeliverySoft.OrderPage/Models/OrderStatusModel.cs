namespace DeliverySoft.OrderPage.Models;

public class OrderStatusModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    /// <summary>
    /// Необходимость простановки комментария при установке данного статуса
    /// </summary>
    public bool RequireComment { get; set; }
}