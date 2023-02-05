using DeliverySoft.DomainServiceEmployees.Dto.Models;
using Entities = DeliverySoft.DAL.Entities;

namespace DeliverySoft.DomainServiceEmployees.DALService.Mapping;

public static class MappingExtensions
{
    public static Employee Map(Entities.Employee employee)
    {
        if (employee == null) return null;
        var result = new Employee();
        result.Id = employee.Id;
        result.FirstName = employee.FirstName;
        result.LastName = employee.LastName;
        result.MiddleName = employee.MiddleName;
        return result;
    }
}