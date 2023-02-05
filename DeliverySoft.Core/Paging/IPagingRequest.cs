using System.Net;

namespace DeliverySoft.Core.Paging;

/// <summary>
/// Абстракция для запроса данных с пагинацией
/// </summary>
public interface IPagingRequest
{
    int PageSize { get; }
    int PageNumber { get; }
}

public static class PagingExtensions
{
    public static void CheckValidPaging(this IPagingRequest pagingRequest)
    {
        if (pagingRequest.PageSize <= 0)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Размер страницы не может быть 0");
        }

        if (pagingRequest.PageNumber < 0)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Номер страницы меньше 0");
        }
    }
}