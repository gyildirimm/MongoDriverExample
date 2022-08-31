using MongoDbDriver.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbDriver.Repositories
{
    public interface IGenericEntityDal<TEntity> : IGenericRepository<TEntity, string>
        where TEntity : class, IEntity<string>, new () 
    {
    }
}
