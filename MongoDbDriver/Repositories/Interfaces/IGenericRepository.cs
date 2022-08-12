using MongoDbDriver.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDriver.Repositories
{
    public interface IGenericRepository<TEntity, in TKey> 
        where TEntity : class, IEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(TKey id);

        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsync(TKey id, TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> DeleteAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TKey id);
        Task<TEntity> DeleteAsync(Expression<Func<TEntity, bool>> filter);
    }

}