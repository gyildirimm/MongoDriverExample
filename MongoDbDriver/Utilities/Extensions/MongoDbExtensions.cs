using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbDriver.Utilities.Extensions
{
    public static class MongoDbExtensions
    {
        public static IMongoCollection<T> GetCollection<T>(this MongoDbSetting setting)
        {
            var client = new MongoClient(setting.ConnectionString);
            var db = client.GetDatabase(setting.Database);

            return db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }

        public static IServiceCollection AddMongoDbSettings(this IServiceCollection services,
           IConfiguration configuration)
        {
            return services.Configure<MongoDbSetting>(options =>
            {
                options.ConnectionString = configuration
                    .GetSection(nameof(MongoDbSetting) + ":" + MongoDbSetting.ConnectionStringValue).Value;
                options.Database = configuration
                    .GetSection(nameof(MongoDbSetting) + ":" + MongoDbSetting.DatabaseValue).Value;
            });
        }
    }
}
