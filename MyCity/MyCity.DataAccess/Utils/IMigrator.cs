namespace MyCity.DataAccess.Utils;

public interface IMigrator
{
    /// <summary>
    ///     Накатить мигрции до последней версии
    /// </summary>
    void Migrate();
}