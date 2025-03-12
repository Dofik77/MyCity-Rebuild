namespace MyCity.DataAccess.Utils;

using MyCity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Класс, представляющий собой реализацию паттерна репозиторий,
/// с помощью которого можно взаимодействовать с данными через EF Core
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TType">Тип id у сущности</typeparam>
public class Repository<TEntity, TType, TDbContext> : IRepository<TEntity>
    where TEntity : BaseEntity<TType>
    where TDbContext : DbContext
{
    protected readonly DbContext _dbCtx;

    public Repository(TDbContext dbCtx)
    {
        _dbCtx = dbCtx;
    }

    /// <summary>
    /// Метод для получения записей с дополнительным условием, например фильтрацией
    /// </summary>
    /// <param name="selector">Дополнительное условие для выборки</param>
    /// <returns>Коллекция моделей по выборке</returns>
    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> selector)
    {
        return _dbCtx.Set<TEntity>().Where(selector).Where(x => x.IsActive).AsQueryable();
    }

    /// <summary>
    /// Метод для получения всех записей
    /// </summary>
    /// <returns>Коллекция всех записей</returns>
    public IQueryable<TEntity> GetAll()
    {
        return _dbCtx.Set<TEntity>().Where(x => x.IsActive).AsQueryable();
    }

    /// <summary>
    /// Метод для получения всех записей вне зависимости от статуса активности
    /// </summary>
    /// <returns>Коллекция всех активных и неактивных записей</returns>
    public IQueryable<TEntity> GetFull()
    {
        return _dbCtx.Set<TEntity>().AsQueryable();
    }

    /// <summary>
    /// Добавление новой записи
    /// </summary>
    /// <param name="newEntity">Модель для добавления</param>
    /// <param name="cancellationToken"></param>
    public async Task AddAsync(TEntity newEntity, CancellationToken cancellationToken)
    {
        await _dbCtx.Set<TEntity>().AddAsync(newEntity, cancellationToken);
    }

    /// <summary>
    /// Добавление коллекции объектов
    /// </summary>
    /// <param name="newEntities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task AddRangeAsync(IEnumerable<TEntity> newEntities, CancellationToken cancellationToken)
    {
        await _dbCtx.Set<TEntity>().AddRangeAsync(newEntities, cancellationToken);
    }

    /// <summary>
    /// Обновление статуса "isActive" на false записи
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public void SoftDelete(TEntity entity)
    {
        _dbCtx.Set<TEntity>().Remove(entity);
    }

    /// <summary>
    /// Обновление статуса "isActive" у коллекции записей
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public void SoftDeleteRange(IEnumerable<TEntity> entities)
    {
        _dbCtx.Set<TEntity>().RemoveRange(entities);
    }

    /// <summary>
    /// Сохранение изменений по произведенным действиям с контекстом
    /// </summary>
    /// <returns></returns>
    public async Task SaveChangesAsync(bool softDeleteEnabled = true)
    {
        var now = DateTime.UtcNow;
        
        var addedEntries = _dbCtx
            .ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Added);
        
        var modifiedEntries = _dbCtx
            .ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Modified);
        
        var deletedEntries = _dbCtx
            .ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Deleted);
        
        foreach (var entry in addedEntries.Where(entry => entry.Entity is IBaseEntity))
        {
            var entity = ((IBaseEntity)entry.Entity);
            entity.DateCreated = now;
            // if (entity is IBaseAuditEntity auditEntity)
            // {
            //     auditEntity.DateUpdated = now;
            // }

            entry.State = EntityState.Added;
        }

        // foreach (var entry in modifiedEntries.Where(entry => entry.Entity is IBaseAuditEntity))
        // {
        //     var entity = ((IBaseAuditEntity)entry.Entity);
        //     entity.DateUpdated = now;
        // }
        //
        // foreach (var entry in deletedEntries.Where(entry => entry.Entity is IBaseAuditEntity))
        // {
        //     var entity = ((IBaseAuditEntity)entry.Entity);
        //     entity.DateDeleted = now;
        // }

        if (softDeleteEnabled)
        {
            foreach (var entry in deletedEntries.Where(entry => entry.Entity is IBaseEntity))
            {
                var entity = ((IBaseEntity) entry.Entity);
                entity.IsActive = false;
                entry.State = EntityState.Modified;
            }
        }

        await _dbCtx.SaveChangesAsync();
    }
}