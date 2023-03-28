using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;

public class EntityFrameworkRepository<TContext, TEntity> : IRepository<TEntity>
    where TEntity : class where TContext : DbContext
{
    public TContext Context { get; }

    public EntityFrameworkRepository(TContext context)
    {
        Context = context;
    }

    protected virtual IQueryable<TEntity> GetQueryable(
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    int? skip = null,
    int? take = null,
    bool disableTracking = true,
    params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.AsExpandable().Where(filter);
        }


        query = includes.Aggregate(query,
             (current, include) => current.Include(include));

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (skip.HasValue && skip.Value > 0)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return query;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        return await GetQueryable(filter, orderBy, skip, take, true, includes).ToListAsync();
    }

    public virtual async Task<TEntity> GetFirstAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includes) => await GetQueryable(filter, orderBy, null, null, true, includes).FirstOrDefaultAsync();

    public virtual async Task<TResult> GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                              Expression<Func<TEntity, bool>> predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool disableTracking = true)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();
        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return orderBy != null
            ? await orderBy(query).Select(selector).FirstOrDefaultAsync()
            : await query.Select(selector).FirstOrDefaultAsync();
    }

    public virtual async Task<IEnumerable<TResult>> GetIncludeAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                            Expression<Func<TEntity, bool>> predicate = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                            bool disableTracking = true)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();
        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            return await orderBy(query).Select(selector).ToListAsync();
        }
        else
        {
            return await query.Select(selector).ToListAsync();
        }
    }

    public virtual void Insert(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    public virtual void InsertRange(IList<TEntity> entities)
    {
        Context.Set<TEntity>().AddRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        Context.Set<TEntity>().Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entity)
    {
        var dbSet = Context.Set<TEntity>();
        if (Context.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }
        dbSet.Remove(entity);
    }

    private bool disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~EntityFrameworkRepository()
    {
        // Finalizer calls Dispose(false)  
        Dispose(false);
    }
}
