using Microsoft.Extensions.Options;
using MongoDbDriver.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbDriver.Repositories
{
    public class GenericEntityDal<TEntity>: MongoGenericRepository<TEntity>, IGenericEntityDal<TEntity>
        where TEntity : MongoBaseEntity, new()
    {
        public GenericEntityDal(IOptions<MongoDbSetting> options): base(options)
        {

        }
    }
}
