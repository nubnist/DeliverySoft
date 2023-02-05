using DeliverySoft.Core;

namespace DeliverySoft.DomainServiceOrders.Dto.Requests;

public class SaveOrderRequest
{
    public int Id { get; set; }
    public EntityParameter<string> DeliveryLocation { get; set; }
    public EntityParameter<string> Title { get; set; }
    public EntityParameter<DateTime> DeliveryDate { get; set; }
    public EntityParameter<string> Comment { get; set; }
    public EntityParameter<int> StatusId { get; set; }
    public EntityParameter<int> ClientId { get; set; }
    public EntityParameter<int[]> EmployeesIds { get; set; }
}