using DeliverySoft.DAL.pgDAL;
using DeliverySoft.DomainService.Helpers;
using DeliverySoft.DomainServiceOrders.DALService;
using DeliverySoft.DomainServiceOrders.WebApi.Settings;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

var dbContextSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
builder.Services.AddSiteDbContextPgSql(connectionString: dbContextSettings.ConnectionString, loggingEnabled: dbContextSettings.LoggingEnabled);

builder.Services.AddDomainServiceOrders();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrdersApi", Version = "v1" });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrdersApi");
});

app.UseRouting();

app.UseCors();
app.MapControllers();

app.Run();