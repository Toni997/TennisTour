using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using TennisTour.Core.Common;
using TennisTour.Core.Exceptions;
using TennisTour.DataAccess.Models;
using TennisTour.DataAccess.Persistence;
using X.PagedList;

namespace TennisTour.DataAccess.Repositories.Impl;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DatabaseContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(DatabaseContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    protected IQueryable<TEntity> Query()
    {
        return _dbSet;
    }

    protected IQueryable<TEntity> Include(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes, IQueryable<TEntity> query)
    {
        return includes != null ?
            includes(query)
            : query;
    }

    protected IQueryable<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, IQueryable<TEntity> query)
    {
        return orderBy != null ? orderBy(query) : query;
    }

    protected IQueryable<TEntity> Expression(Expression<Func<TEntity, bool>> expression, IQueryable<TEntity> query)
    {
        return expression != null ? query.Where(expression) : query;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = (await _dbSet.AddAsync(entity)).Entity;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Handle or log the exception
            Console.WriteLine(ex.Message);
            // Additional handling as needed
        }

        return addedEntity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var removedEntity = _dbSet.Remove(entity).Entity;
        await _context.SaveChangesAsync();

        return removedEntity;
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

        return entity;
    }

    public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            int? take = null)
    {
        var query = Query();

        query = Expression(expression, query);
        query = Include(includes, query);
        query = OrderBy(orderBy, query);

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<IPagedList<TEntity>> GetAllPagedAsync(PagedRequestParams requestParams,
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
    {
        var query = Query();

        query = Expression(expression, query);
        query = Include(includes, query);
        query = OrderBy(orderBy, query);

        return await query.ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
    }

    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>,
        IIncludableQueryable<TEntity, object>> includes = null)
    {
        var query = Query();
        query = Include(includes, query);

        var entity = await query.FirstOrDefaultAsync(expression);

        if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

        return entity;
    }

    public async Task<TEntity> GetOneOrNullAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>,
       IIncludableQueryable<TEntity, object>> includes = null)
    {
        var query = Query();
        query = Include(includes, query);

        var entity = await query.FirstOrDefaultAsync(expression);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.FindAsync(id) != null;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
