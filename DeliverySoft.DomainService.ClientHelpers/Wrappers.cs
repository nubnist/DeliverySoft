using DeliverySoft.Core;
using DeliverySoft.Core.Models;
using Newtonsoft.Json;

namespace DeliverySoft.DomainService.ClientHelpers;

/// <summary>
/// Класс предоставляет статические методы для оберки запросов к сервисам
/// </summary>
public static class Wrappers
{
   public static string GetUrl(string[] paths)
            => paths.Aggregate((current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}");

    public static async Task<T> SendRequest<T>(Func<Task<Refit.ApiResponse<T>>> method)
    {
        var response = await method();
        var isSuccess = response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent;
        if (!isSuccess)
        {
            ApiError apiError = null;
            try
            {
                apiError = JsonConvert.DeserializeObject<ApiError>(response.Error.Content);
            }
            catch (Exception) { }
            if (apiError != null && apiError.ErrorId != Guid.Empty)
            {
                throw new ApiException(response.StatusCode, apiError.Message);
            }
            throw new ApiException(response.StatusCode, "Refit exception");
        }
        return response.Content;
    }

    public static async Task SendRequest(Func<Task> method)
    {
        try
        {
            await method();
        }
        catch (Refit.ApiException ex)
        {
            if (ex.StatusCode != System.Net.HttpStatusCode.OK)
            {
                ApiError apiError = null;
                try
                {
                    apiError = JsonConvert.DeserializeObject<ApiError>(ex.Content);
                }
                catch (Exception) { }
                if (apiError != null && apiError.ErrorId != Guid.Empty)
                {
                    throw new ApiException(ex.StatusCode, apiError.Message);
                }
                throw new ApiException(ex.StatusCode, "Refit exception");
            }
        }
    }
}