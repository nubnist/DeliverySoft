using DeliverySoft.Core;

namespace DeliverySoft.DomainServiceClients.Dto.Requests;

public class SaveClientRequest
{
    public int Id { get; set; }
    public EntityParameter<string> Name { get; set; }
}