using Binbin.Linq;
using Microsoft.EntityFrameworkCore;
using Entities = DeliverySoft.DAL.Entities;

namespace DeliverySoft.DomainServiceOrders.DALService.Helpers;

public static class OrdersHelpersExtension
{
    /// <summary>
    /// Набоор символов, по которым будет делиться строка на слова
    /// </summary>
    private static readonly char[] splitChars = { ' ', ',' };
    
    /// <summary>
    /// Нечеткий поиск. Поисковой запрос разделяется на слова, а далее каждое слово ищется на вхождения в базе
    /// </summary>
    public static IQueryable<Entities.Order> SearchStringOrderHelper(this IQueryable<Entities.Order> unworkTimeQuery, string search)
    {
        search = search?.Trim();

        if (string.IsNullOrEmpty(search))
        {
            return unworkTimeQuery;
        }

        var words = search.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);

        var prAnd = PredicateBuilder.True<Entities.Order>();

        foreach (var word in words)
        {
            var prOr = PredicateBuilder.False<Entities.Order>();

            prOr = prOr.Or(e => EF.Functions.Like(e.Comment, $"%{word}%"));
            prOr = prOr.Or(e => EF.Functions.Like(e.Title, $"%{word}%"));
            prOr = prOr.Or(e => EF.Functions.Like(e.Client.Name, $"%{word}%"));
            prOr = prOr.Or(e => EF.Functions.Like(e.Status.Title, $"%{word}%"));
            prOr = prOr.Or(e => EF.Functions.Like(e.DeliveryLocation, $"%{word}%"));

            prAnd = prAnd.And(prOr);
        }
            
        unworkTimeQuery = unworkTimeQuery.Where(prAnd);

        return unworkTimeQuery;
    }
}