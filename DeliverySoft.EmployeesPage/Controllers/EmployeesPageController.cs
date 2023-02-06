using DeliverySoft.EmployeesPage.Models;
using DeliverySoft.EmployeesPage.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySoft.EmployeesPage.Controllers
{
    /// <summary>
    /// Страница сотрудников
    /// </summary>
    [ApiController, Route("api/[controller]")]
    public class EmployeesPageController : ControllerBase
    {
        private EmployeesPage EmployeesPage { get; }
        public EmployeesPageController(EmployeesPage employeesPage)
        {
            this.EmployeesPage = employeesPage;
        }

        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        [HttpGet("[action]")]
        public Task<EmployeeModel[]> GetEmployees(CancellationToken cancellationToken = default)
            => this.EmployeesPage.GetEmployees(cancellationToken);
        
        /// <summary>
        /// Сохранить сотрудника
        /// </summary>
        [HttpPost("[action]")]
        public Task<int> SaveEmployee([FromBody] SaveEmployeeRequest request)
            => this.EmployeesPage.SaveEmployee(request);
        
        
    }
}
