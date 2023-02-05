using DeliverySoft.DomainServiceClients.Dto.Models;
using Entities = DeliverySoft.DAL.Entities;

namespace DeliverySoft.DomainServiceClients.DALService.Mapping;

public static class MappingExtensions
{
    public static Client Map(Entities.Client client)
    {
        if (client == null) return null;
        var result = new Client();
        result.Id = client.Id;
        result.Name = client.Name;
        return result;
    }
}