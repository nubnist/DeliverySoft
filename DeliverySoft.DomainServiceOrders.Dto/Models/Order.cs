namespace DeliverySoft.DomainServiceOrders.Dto.Models;
 
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
 
     public int[] EmployeesIds { get; set; }
 }