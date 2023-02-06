namespace DeliverySoft.EmployeesPage.Requests;

public class SaveEmployeeRequest
{
    /// <summary>
    /// Если необходимо создать новую запись - передается 0
    /// </summary>
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
}