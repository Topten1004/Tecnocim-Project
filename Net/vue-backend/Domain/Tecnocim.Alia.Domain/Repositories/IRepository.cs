using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Tecnocim.Alia.Domain.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<TEntity, object>>[] includes);
    
    Task<TEntity> GetFirstAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includes);

    Task<TResult> GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                              Expression<Func<TEntity, bool>> predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool disableTracking = true);

    Task<IEnumerable<TResult>> GetIncludeAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                            Expression<Func<TEntity, bool>> predicate = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                            bool disableTracking = true);

    void Insert(TEntity entity);

    void InsertRange(IList<TEntity> entities);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}
