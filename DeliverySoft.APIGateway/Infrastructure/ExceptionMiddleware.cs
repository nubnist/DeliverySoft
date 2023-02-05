using System.Net;
using DeliverySoft.Core;
using DeliverySoft.Core.Models;
using Newtonsoft.Json;

namespace DeliverySoft.APIGateway.Infrastructure;

public class ExceptionMiddleware
{
    private RequestDelegate Next { get; }

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.Next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await Next(httpContext);
        }
        catch (Exception exception)
        {
            await ProceedException(httpContext, exception);
        }
    }

    private async Task ProceedException(HttpContext context, Exception error)
    {
        switch (error)
        {
            case ApiException exception:
            {
                context.Response.StatusCode = (int)exception.StatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(exception.GetApiError()));
                break;
            }
            default:
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Guid errorId = Guid.NewGuid();
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ApiError(HttpStatusCode.InternalServerError, errorId, "Internal Server Error.")));
                break;
            }
        }
    }
}