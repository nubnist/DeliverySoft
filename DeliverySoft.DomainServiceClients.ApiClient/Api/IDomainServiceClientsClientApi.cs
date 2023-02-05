using DeliverySoft.DomainServiceClients.Dto.Models;
using DeliverySoft.DomainServiceClients.Dto.Requests;
using DeliverySoft.DomainServiceClients.WebContracts;
using Refit;

namespace DeliverySoft.DomainServiceClients.ApiClient.Api;

public interface IDomainServiceClientsClientApi
{
    [Post("/GetClients")]
    Task<ApiResponse<Client[]>> GetClients([Body] GetClientsWebContract getClientsWebContract, CancellationToken cancellationToken);
   
    [Post("/SaveClient")]
    Task<ApiResponse<int>> SaveClient([Body] SaveClientRequest request);
}