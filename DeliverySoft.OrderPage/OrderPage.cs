using System.Net;
using DeliverySoft.Core;
using DeliverySoft.DomainServiceClients.ApiClient.Interfaces;
using DeliverySoft.OrderPage.Requests;
using ClientsDto = DeliverySoft.DomainServiceClients.Dto;

namespace DeliverySoft.OrderPage;

public class OrderPage
{
    private IDomainServiceClientsClient ClientsClient { get; }
    
    public OrderPage(IDomainServiceClientsClient clientsClient)
    {
        ClientsClient = clientsClient;
    }

    public async Task<int> SaveClient(SaveClientRequest request, CancellationToken cancellationToken)
    {
        var saveRequest = new ClientsDto.Requests.SaveClientRequest();
        if (request.Id != 0)
        {
            _ = await this.ClientsClient.GetClients(ids: new ArrayFilter<int>(false, new[] { request.Id }), cancellationToken: cancellationToken)
                .ContinueWith(t => t.Result.FirstOrDefault()) ?? throw new ApiException(HttpStatusCode.BadRequest, "Указанный клиент не существует");
        }

        saveRequest.Id = request.Id;
        saveRequest.Name = request.Name;

        var clientId = await this.ClientsClient.SaveClient(saveRequest);

        return clientId;
    }
}