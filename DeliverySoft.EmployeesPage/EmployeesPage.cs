using System.Net;
using DeliverySoft.Core;
using DeliverySoft.DomainServiceEmployees.ApiClient.Interfaces;
using DeliverySoft.EmployeesPage.Models;
using DeliverySoft.EmployeesPage.Requests;
using EmployeesDto = DeliverySoft.DomainServiceEmployees.Dto;

namespace DeliverySoft.EmployeesPage;

public class EmployeesPage
{
    private IDomainServiceEmployeesClient EmployeesClient { get; }
    
    public EmployeesPage(IDomainServiceEmployeesClient employeesClient)
    {
        this.EmployeesClient = employeesClient;
    }

    public async Task<int> SaveEmployee(SaveEmployeeRequest request)
    {
        var saveRequest = new EmployeesDto.Requests.SaveEmployeeRequest();
        if (request.Id != 0)
        {
            _ = await this.EmployeesClient.GetEmployees(ids: new ArrayFilter<int>(false, new[] { request.Id }))
                .ContinueWith(t => t.Result.FirstOrDefault()) ?? throw new ApiException(HttpStatusCode.BadRequest, "Указанный сотрудник не существует");
        }

        saveRequest.Id = request.Id;
        saveRequest.FirstName = request.FirstName;
        saveRequest.LastName = request.LastName;
        saveRequest.MiddleName = request.MiddleName;

        var employeeId = await this.EmployeesClient.SaveEmployee(saveRequest);

        return employeeId;
    }

    public async Task<EmployeeModel[]> GetEmployees(CancellationToken cancellationToken)
    {
        var employees = await this.EmployeesClient.GetEmployees(cancellationToken: cancellationToken);

        return employees.Select(c => new EmployeeModel()
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            MiddleName = c.MiddleName,
        }).ToArray();
    }
}