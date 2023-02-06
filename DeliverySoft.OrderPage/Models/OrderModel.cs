namespace DeliverySoft.OrderPage.Models;

public class OrderModel
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
    
    public OrderStatusModel Status { get; set; }
    
    public ClientModel Client { get; set; }
    public EmployeeModel[] Employees { get; set; }

    /// <summary>
    /// Разрешено ли редактировать
    /// </summary>
    public bool IsAllowedChange { get; set; }
    /// <summary>
    /// Требуется ли комментарий при удалении
    /// </summary>
    public bool NeedCommentOnDelete { get; set; }
}