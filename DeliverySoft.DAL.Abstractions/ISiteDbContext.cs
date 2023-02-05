using DeliverySoft.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MovieList.DAL.Abstractions;

public interface ISiteDbContext
{
    DatabaseFacade Database { get; }
    
    DbSet<Client> Clients { get; }
    DbSet<Employee> Employees { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderStatus> OrderStatuses { get; }
    DbSet<AppointedEmployee> AppointedEmployees { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}