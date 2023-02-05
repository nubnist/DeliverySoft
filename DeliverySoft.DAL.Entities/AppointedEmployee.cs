namespace DeliverySoft.DAL.Entities;

/// <summary>
/// Сотрудник назначенный в заказ
/// </summary>
public class AppointedEmployee
{
    public int OrderId { get; set; }
    public int EmployeeId { get; set; }

    public Order Order { get; set; }
    public Employee Employee { get; set; }
}