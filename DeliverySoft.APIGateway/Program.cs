using DeliverySoft.APIGateway.Infrastructure;
using DeliverySoft.DomainServiceClients.ApiClient;
using DeliverySoft.DomainServiceEmployees.ApiClient;
using DeliverySoft.DomainServiceOrders.ApiClient;
using DeliverySoft.OrderPage;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

#region Pages
builder.Services.AddOrderPage();
#endregion

#region DomainServices
builder.Services.AddDomainServiceEmployeesClient(builder.Configuration.GetConnectionString("DomainServiceEmployees"));
builder.Services.AddDomainServiceClientsClient(builder.Configuration.GetConnectionString("DomainServiceClients"));
builder.Services.AddDomainServiceOrdersClient(builder.Configuration.GetConnectionString("DomainServiceOrders"));
#endregion

JsonConvert.DefaultSettings = () =>
{
    return new JsonSerializerSettings()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };
};

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DeliverySoftApi", Version = "v1" });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();
#endregion

//=======================
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();