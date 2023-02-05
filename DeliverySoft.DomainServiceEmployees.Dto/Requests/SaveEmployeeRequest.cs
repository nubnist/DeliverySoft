using DeliverySoft.Core;

namespace DeliverySoft.DomainServiceEmployees.Dto.Requests;

public class SaveEmployeeRequest
{
    public int Id { get; set; }
    public EntityParameter<string> FirstName { get; set; }
    public EntityParameter<string> LastName { get; set; }
    public EntityParameter<string> MiddleName { get; set; }
}