using System.Reflection;
using DeliverySoft.APIGateway.Helpers;
using DeliverySoft.APIGateway.Infrastructure;
using DeliverySoft.Core;
using DeliverySoft.DomainServiceClients.ApiClient;
using DeliverySoft.DomainServiceEmployees.ApiClient;
using DeliverySoft.DomainServiceOrders.ApiClient;
using DeliverySoft.EmployeesPage;
using DeliverySoft.OrderPage;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

#region Pages
builder.Services.AddOrderPage();
builder.Services.AddEmployeesPage();
#endregion

#region Domain Services
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
    c.CustomSchemaIds(x => SwaggerHelper.GetNameType(x).ToString());
    
    var executionFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    List<string> xmlCommentFiles = new List<string>();
    XmlCommentHelpers.FillFilesByPattern(executionFolder, xmlCommentFiles, "DeliverySoft.*.xml");

    foreach (var xmlFile in xmlCommentFiles.GroupBy(Path.GetFileName).Select(fg => fg.First())) // Убираю дублирующиеся файлы
    {
        c.IncludeXmlComments(xmlFile); // Подключаю файлы с документацией к API
    }
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