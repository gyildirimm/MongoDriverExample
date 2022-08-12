using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbDriver.Entities;
using MongoDbDriver.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDriver.Repositories
{
    public class MongoGenericRepository<TEntity> : IGenericRepository<TEntity, string>
        where TEntity : MongoBaseEntity, new()
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoGenericRepository(IOptions<MongoDbSetting> options)
        {
            _collection = options.Value.GetCollection<TEntity>();
        }

        #region Private Methods
        private static InsertOneOptions GetInsertOneOptions()
        {
            return new InsertOneOptions { BypassDocumentValidation = false };
        }

        private static BulkWriteOptions GetBulkWriteOptions()
        {
            return new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
        }
        #endregion

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null
                ? _collection.AsQueryable()
                : _collection.AsQueryable().Where(predicate);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _collection.Find(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity, GetInsertOneOptions());
            return entity;
        }

        public virtual async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return (await _collection.BulkWriteAsync((IEnumerable<WriteModel<TEntity>>)entities, GetBulkWriteOptions())).IsAcknowledged;
        }

        public virtual async Task<TEntity> UpdateAsync(string id, TEntity entity)
        {
            return await _collection.FindOneAndReplaceAsync(x => x.Id == id, entity);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            return await _collection.FindOneAndReplaceAsync(predicate, entity);
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            return await _collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
        }

        public virtual async Task<TEntity> DeleteAsync(string id)
        {
            return await _collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _collection.FindOneAndDeleteAsync(filter);
        }
    }
}
