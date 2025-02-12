using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCity.DataAccess.Models;

namespace MyCity.DataAccess.Utils;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Добавление контекстов БД и вспомогательных классов
    /// </summary>
    public static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connection = configuration.GetValue<string>("ConnectionStrings:Main");
        
        services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
        // services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationContext>>();
        // services.AddScoped<IMigrator, Migrator>();
    
        return services;
    }
}