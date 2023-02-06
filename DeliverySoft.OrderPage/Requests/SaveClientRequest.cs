namespace DeliverySoft.OrderPage.Requests;

public class SaveClientRequest
{
    /// <summary>
    /// Если необходимо создать новую запись - передается 0
    /// </summary>
    public int Id { get; set; }
    public string Name { get; set; }
}