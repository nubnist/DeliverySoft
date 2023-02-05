using System.Net;
using DeliverySoft.Core;
using DeliverySoft.DomainService.Helpers;
using DeliverySoft.DomainServiceClients.DALService.Mapping;
using DeliverySoft.DomainServiceClients.Dto;
using DeliverySoft.DomainServiceClients.Dto.Models;
using DeliverySoft.DomainServiceClients.Dto.Requests;
using Microsoft.EntityFrameworkCore;
using MovieList.DAL.Abstractions;
using Entities = DeliverySoft.DAL.Entities;

namespace DeliverySoft.DomainServiceClients.DALService;

public class ClientService : IClientService
{
    private ISiteDbContext SiteDbContext { get; }

    public ClientService(ISiteDbContext siteDbContext)
    {
        this.SiteDbContext = siteDbContext;
    }


    public async Task<Client[]> GetClients(ArrayFilter<int> ids = null, 
                                           GetClientsRequest request = null, 
                                           PaginationOptions pagination = default,
                                           CancellationToken cancellationToken = default)
    {
        IQueryable<Entities.Client> query = this.SiteDbContext.Clients;

        if (ids != null)
        {
            query = ids.Inverted 
                ? query.Where(e => !ids.Values.Contains(e.Id)) 
                : query.Where(e => ids.Values.Contains(e.Id));
        }
        
        query = query.PaginationQuery(pagination);

        var result = await query.ToArrayAsync(cancellationToken);
        return result.Select(MappingExtensions.Map).ToArray();
    }

    public async Task<int> SaveClient(SaveClientRequest request)
    {
        Entities.Client client;
        if (request.Id == 0)
        {
            client = new Entities.Client();
            this.SiteDbContext.Clients.Add(client);
        }
        else
        {
            client = await this.SiteDbContext.Clients.FirstOrDefaultAsync(v => v.Id == request.Id);
            if (client == null)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, "Указанный клиент не найден");
            }
        }

        if (request.Name?.IsDefined == true) client.Name = request.Name.Value;

        await this.SiteDbContext.SaveChangesAsync();

        return client.Id;
    }
}