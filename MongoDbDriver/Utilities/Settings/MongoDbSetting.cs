using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbDriver
{
    public class MongoDbSetting
    {
        public string ConnectionString;
        public string Database;


        public const string ConnectionStringValue = nameof(ConnectionString);
        public const string DatabaseValue = nameof(Database);
    }
}
