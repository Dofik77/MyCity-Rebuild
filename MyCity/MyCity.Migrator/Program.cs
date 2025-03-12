using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCity.DataAccess.Utils;

namespace MyCity.Migrator;

internal class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = CreateServices();

        using var scope = serviceProvider.CreateScope();

        var migrator = scope.ServiceProvider.GetRequiredService<IMigrator>();

        migrator.Migrate();
    }

    private static IServiceProvider CreateServices()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.Development.json", true)
            .AddEnvironmentVariables()
            .Build();

        return new ServiceCollection()
            .AddDbContexts(configuration)
            .BuildServiceProvider(false);
    }
}
