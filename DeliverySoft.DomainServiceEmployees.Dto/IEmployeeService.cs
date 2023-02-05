using DeliverySoft.Core;
using DeliverySoft.DomainService.CommonDTOs;
using DeliverySoft.DomainServiceEmployees.Dto.Models;
using DeliverySoft.DomainServiceEmployees.Dto.Requests;

namespace DeliverySoft.DomainServiceEmployees.Dto;

public interface IEmployeeService
{
    /// <summary>
    /// Получить список сотрудников
    /// </summary>
    /// <param name="ids">Фильтр по id сотрудников</param>
    /// <param name="request">Дополнительные параметры выборки</param>
    Task<Employee[]> GetEmployees(ArrayFilter<int> ids = null, GetEmployeesRequest request = null, PaginationOptions pagination = null, CancellationToken cancellationToken = default);
    
    Task<int> SaveEmployee(SaveEmployeeRequest request);
}