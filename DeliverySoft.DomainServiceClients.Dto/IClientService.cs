using DeliverySoft.Core;
using DeliverySoft.DomainServiceClients.Dto.Models;
using DeliverySoft.DomainServiceClients.Dto.Requests;

namespace DeliverySoft.DomainServiceClients.Dto;

public interface IClientService
{
    Task<Client[]> GetClients(ArrayFilter<int> ids = null, GetClientsRequest request = null, PaginationOptions pagination = default, CancellationToken cancellationToken = default);
    
    Task<int> SaveClient(SaveClientRequest request);
}