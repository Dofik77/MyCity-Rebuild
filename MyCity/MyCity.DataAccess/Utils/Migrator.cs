using Microsoft.EntityFrameworkCore;

namespace MyCity.DataAccess.Utils;

internal class Migrator : IMigrator
{
    private readonly ApplicationContext _applicationContext;

    public Migrator(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public void Migrate()
    {
        _applicationContext.Database.Migrate();
    }
}