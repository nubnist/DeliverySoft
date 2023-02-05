using DeliverySoft.DomainServiceEmployees.Dto;
using DeliverySoft.DomainServiceEmployees.Dto.Models;
using DeliverySoft.DomainServiceEmployees.WebContracts;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySoft.DomainServiceEmployees.WebApi.Controllers;

[ApiController, Route("api/[controller]")]
public class EmployeeServiceController : ControllerBase
{
    public IEmployeeService EmployeeService { get; set; }
    public EmployeeServiceController(IEmployeeService employeeService)
    {
        this.EmployeeService = employeeService;
    }
    

    [HttpPost("GetEmployees")]
    public Task<Employee[]> GetEmployees([FromBody] GetEmployeesWebContract request, CancellationToken cancellationToken)
        => this.EmployeeService.GetEmployees(request.Ids, request.Request, cancellationToken);
}