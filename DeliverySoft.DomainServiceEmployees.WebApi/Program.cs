using DeliverySoft.DAL.pgDAL;
using DeliverySoft.DomainService.Helpers;
using DeliverySoft.DomainServiceEmployees.DALService;
using DeliverySoft.DomainServiceEmployees.WebApi.Settings;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

var dbContextSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
builder.Services.AddSiteDbContextPgSql(connectionString: dbContextSettings.ConnectionString, loggingEnabled: dbContextSettings.LoggingEnabled);

builder.Services.AddDomainServiceEmployees();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeesApi", Version = "v1" });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeesApi");
});

app.UseRouting();

app.UseCors();
app.MapControllers();

app.Run();