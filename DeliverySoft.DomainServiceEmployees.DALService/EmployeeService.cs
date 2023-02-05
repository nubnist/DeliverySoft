using System.Net;
using DeliverySoft.Core;
using DeliverySoft.DomainServiceEmployees.DALService.Mapping;
using DeliverySoft.DomainServiceEmployees.Dto;
using DeliverySoft.DomainServiceEmployees.Dto.Models;
using DeliverySoft.DomainServiceEmployees.Dto.Requests;
using Microsoft.EntityFrameworkCore;
using MovieList.DAL.Abstractions;
using Entities = DeliverySoft.DAL.Entities;

namespace DeliverySoft.DomainServiceEmployees.DALService;

public class EmployeeService : IEmployeeService
{
    private ISiteDbContext SiteDbContext { get; }

    public EmployeeService(ISiteDbContext siteDbContext)
    {
        this.SiteDbContext = siteDbContext;
    }
    
    public async Task<Employee[]> GetEmployees(ArrayFilter<int> ids, 
                                               GetEmployeesRequest request, 
                                               CancellationToken cancellationToken)
    {
        IQueryable<Entities.Employee> query = this.SiteDbContext.Employees;

        if (ids != null)
        {
            query = ids.Inverted 
                ? query.Where(e => !ids.Values.Contains(e.Id)) 
                : query.Where(e => ids.Values.Contains(e.Id));
        }
        

        var result = await query.ToArrayAsync(cancellationToken);
        return result.Select(MappingExtensions.Map).ToArray();
    }

    public async Task<int> SaveEmployee(SaveEmployeeRequest request)
    {
        Entities.Employee employee;
        if (request.Id == 0)
        {
            employee = new Entities.Employee();
            this.SiteDbContext.Employees.Add(employee);
        }
        else
        {
            employee = await this.SiteDbContext.Employees.FirstOrDefaultAsync(v => v.Id == request.Id);
            if (employee == null)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, "Указанный сотрудник не найден");
            }
        }

        if (request.FirstName?.IsDefined == true) employee.FirstName = request.FirstName.Value;
        if (request.LastName?.IsDefined == true) employee.LastName = request.LastName.Value;
        if (request.MiddleName?.IsDefined == true) employee.MiddleName = request.MiddleName.Value;

        await this.SiteDbContext.SaveChangesAsync();

        return employee.Id;
    }
}