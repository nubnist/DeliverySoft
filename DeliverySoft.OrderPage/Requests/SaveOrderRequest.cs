namespace DeliverySoft.OrderPage.Requests;

public class SaveOrderRequest
{
    /// <summary>
    /// Если необходимо создать новую запись - передается 0
    /// </summary>
    public int Id { get; set; }
    public string DeliveryLocation { get; set; }
    public string Title { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string? Comment { get; set; }

    public int StatusId { get; set; }
    public int ClientId { get; set; }
    
    public int[] EmployeesIds { get; set; }
}