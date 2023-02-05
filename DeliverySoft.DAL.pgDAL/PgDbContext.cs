using DeliverySoft.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using MovieList.DAL.Abstractions;

namespace DeliverySoft.DAL.pgDAL;

public class PgDbContext : DbContext, ISiteDbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set;  }
    public DbSet<AppointedEmployee> AppointedEmployees { get; set; }

    public PgDbContext(DbContextOptions<PgDbContext> options) : base(options)
    { }
    
    private void ConfigureClients(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder
            .Entity<Client>()
            .ToTable("client");

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("id");
        
        entity.Property(e => e.Name).HasColumnName("name");
    }
    
    private void ConfigureEmployees(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder
            .Entity<Employee>()
            .ToTable("employee");

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("id");
        
        entity.Property(e => e.FirstName).HasColumnName("first_name");
        entity.Property(e => e.LastName).HasColumnName("last_name");
        entity.Property(e => e.MiddleName).HasColumnName("middle_name");
    }
    
    private void ConfigureOrders(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder
            .Entity<Order>()
            .ToTable("order");

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("id");
        
        entity.Property(e => e.DeliveryLocation).HasColumnName("delivery_location");
        entity.Property(e => e.Title).HasColumnName("title");
        entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
        entity.Property(e => e.Comment).HasColumnName("comment");
        
        entity.Property(e => e.ClientId).HasColumnName("client_id");
        entity.HasOne(e => e.Client)
              .WithMany(e => e.Orders)
              .HasForeignKey(e => e.ClientId);
        
        entity.Property(e => e.StatusId).HasColumnName("status_id");
        entity.HasOne(e => e.Status)
              .WithMany(e => e.Orders)
              .HasForeignKey(e => e.StatusId);
    }
    
    private void ConfigureOrderStatuses(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder
            .Entity<OrderStatus>()
            .ToTable("order_status");

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("id");
        
        entity.Property(e => e.Title).HasColumnName("title");
        entity.Property(e => e.AllowedChangeOrderData).HasColumnName("allowed_change_order_data");
        entity.Property(e => e.RequireComment).HasColumnName("require_comment");
    }
    
    private void ConfigureAppointedEmployees(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder
            .Entity<AppointedEmployee>()
            .ToTable("appointed_employee");

        entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
        entity.HasOne(d => d.Employee)
            .WithMany(p => p.AppointedEmployees)
            .HasForeignKey(d => d.EmployeeId);

        entity.Property(e => e.OrderId).HasColumnName("order_id");
        entity.HasOne(d => d.Order)
            .WithMany(p => p.AppointedEmployees)
            .HasForeignKey(d => d.EmployeeId);

        entity.HasKey(e => new { e.OrderId, e.EmployeeId });
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureClients(modelBuilder);
        ConfigureEmployees(modelBuilder);
        ConfigureOrders(modelBuilder);
        ConfigureAppointedEmployees(modelBuilder);
        ConfigureOrderStatuses(modelBuilder);
    }
}