using Microsoft.EntityFrameworkCore;
using MyCity.DataAccess.Models;

namespace MyCity.DataAccess.Utils;


/// <inheritdoc cref="IUnitOfWork" />
internal class UnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    private readonly TDbContext _applicationContext;
    private bool _disposed;

    public UnitOfWork(
        TDbContext applicationContext,
        IRepository<Location> locationRepository)
    {
        _applicationContext = applicationContext;
        LocationRepository = locationRepository;
    }

    public IRepository<Location> LocationRepository { get; }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken, bool softDeleteEnabled = true)
    {
        var now = DateTime.Now.ToUniversalTime();
        
        var addedEntries = _applicationContext
            .ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Added);
        var modifiedEntries = _applicationContext
            .ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Modified);
        
        var deletedEntries = _applicationContext
            .ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Deleted);
        
        
        foreach (var entry in addedEntries.Where(entry => entry.Entity is IBaseEntity))
        {
            var entity = (IBaseEntity) entry.Entity;
            entity.DateCreated = now;
            // if (entity is IBaseAuditEntity auditEntity)
            //     auditEntity.DateUpdated = now;
            
            entry.State = EntityState.Added;
        }
        
        // foreach (var entry in modifiedEntries.Where(entry => entry.Entity is IBaseAuditEntity))
        // {
        //     var entity = (IBaseAuditEntity) entry.Entity;
        //     entity.DateUpdated = now;
        // }

        if (softDeleteEnabled)
            foreach (var entry in deletedEntries.Where(entry => entry.Entity is IBaseEntity))
            {
                var entity = (IBaseEntity) entry.Entity;
                entity.IsActive = false;
                entry.State = EntityState.Modified;
            }

        return _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public void ClearTrackers()
    {
        _applicationContext.ChangeTracker.Clear();
    }

    public void SetCommandTimeout(int? timeout)
    {
        _applicationContext.Database.SetCommandTimeout(timeout);
    }

    public virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                _applicationContext.Dispose();

            _disposed = true;
        }
    }
}