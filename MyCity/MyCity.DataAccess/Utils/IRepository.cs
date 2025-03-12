namespace MyCity.DataAccess.Utils;

using System.Linq.Expressions;

public interface IRepository<TEntity>
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> selector);
    
    IQueryable<TEntity> GetAll();
    
    IQueryable<TEntity> GetFull();
    
    Task AddAsync(TEntity newEntity, CancellationToken cancellationToken);
    
    Task AddRangeAsync(IEnumerable<TEntity> newEntities, CancellationToken cancellationToken);
    
    void SoftDelete(TEntity entity);
    
    void SoftDeleteRange(IEnumerable<TEntity> entities);
    
    Task SaveChangesAsync(bool softDeleteEnabled = true);
}
