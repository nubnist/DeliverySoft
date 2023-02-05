using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieList.DAL.Abstractions;

namespace DeliverySoft.DAL.pgDAL;

public static class PgDbExtensions
{
    public static IServiceCollection AddSiteDbContextPgSql(this IServiceCollection services,
                                                           string connectionString,
                                                           bool loggingEnabled)
    {
        services.AddDbContext<ISiteDbContext, PgDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseNpgsql(connectionString);
            if (loggingEnabled)
            {
                optionsBuilder.EnableSensitiveDataLogging();
            }
        });
        return services;
    }
}