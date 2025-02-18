using MyCity.DataAccess.Models;

namespace MyCity.DataAccess.Utils;

/// <summary>
///     Интерфейc для работы с БД
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    ///     Репозиторий для работы с атрибутами документов
    /// </summary>
    public IRepository<Location> LocationRepository { get; }

    /// <summary>
    ///     Применить транзакцию
    /// </summary>
    Task<int> SaveEntitiesAsync(CancellationToken cancellationToken, bool softDeleteEnabled = true);

    /// <summary>
    ///     Очистка трекинга записей
    /// </summary>
    void ClearTrackers();

    /// <summary>
    ///     Поставить таймаут для команд sql
    /// </summary>
    void SetCommandTimeout(int? timeout);
}