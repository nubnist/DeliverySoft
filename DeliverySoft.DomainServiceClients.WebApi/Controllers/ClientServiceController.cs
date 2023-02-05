using DeliverySoft.DomainServiceClients.Dto;
using DeliverySoft.DomainServiceClients.Dto.Models;
using DeliverySoft.DomainServiceClients.Dto.Requests;
using DeliverySoft.DomainServiceClients.WebContracts;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySoft.DomainServiceClients.WebApi.Controllers;

[ApiController, Route("api/[controller]")]
public class ClientServiceController : ControllerBase
{
    public IClientService EmployeeService { get; set; }
    public ClientServiceController(IClientService employeeService)
    {
        this.EmployeeService = employeeService;
    }

    [HttpPost("GetClients")]
    public Task<Client[]> GetEmployees([FromBody] GetClientsWebContract request, CancellationToken cancellationToken)
        => this.EmployeeService.GetClients(request.Ids, request.Request, request.Pagination, cancellationToken);
    
    [HttpPost("SaveClient")]
    public Task<int> SaveEmployee(SaveClientRequest request)
        => this.EmployeeService.SaveClient(request);
}