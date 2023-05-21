using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TennisTour.Core.Common;
using TennisTour.DataAccess.Models;
using X.PagedList;

namespace TennisTour.DataAccess.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetByIdAsync(Guid id);

    Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>,
        IIncludableQueryable<TEntity, object>> includes = null);

    Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

    Task<IPagedList<TEntity>> GetAllPagedAsync(PagedRequestParams requestParams,
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<TEntity> DeleteAsync(TEntity entity);
    Task<bool> ExistsAsync(Guid id);
}
