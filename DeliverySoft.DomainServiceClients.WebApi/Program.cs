using DeliverySoft.DAL.pgDAL;
using DeliverySoft.DomainService.Helpers;
using DeliverySoft.DomainServiceClients.DALService;
using DeliverySoft.DomainServiceClients.WebApi.Settings;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

var dbContextSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
builder.Services.AddSiteDbContextPgSql(connectionString: dbContextSettings.ConnectionString, loggingEnabled: dbContextSettings.LoggingEnabled);

builder.Services.AddDomainServiceClient();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientsApi", Version = "v1" });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientsApi");
});

app.UseRouting();

app.UseCors();
app.MapControllers();

app.Run();