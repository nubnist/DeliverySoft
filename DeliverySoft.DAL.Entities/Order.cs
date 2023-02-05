using System.Security.AccessControl;

namespace DeliverySoft.DAL.Entities;

public class Order
{
    public int Id { get; set; }
    /// <summary>
    /// Адрес доставки
    /// </summary>
    public string DeliveryLocation { get; set; }
    /// <summary>
    /// Заголовок заказа
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Дата доставки
    /// </summary>
    public DateTime DeliveryDate { get; set; }
    public string Comment { get; set; }

    public int StatusId { get; set; }
    public OrderStatus Status { get; set; }
    
    public int ClientId { get; set; }
    public Client Client { get; set; }

    /// <summary>
    /// Назначенные курьеры
    /// </summary>
    public ICollection<AppointedEmployee> AppointedEmployees { get; set; } = new List<AppointedEmployee>();
}