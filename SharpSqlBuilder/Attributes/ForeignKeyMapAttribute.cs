using System;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Attributes
{
    public class SqlDatabaseAttribute : Attribute
    {
        public readonly SqlDatabase SqlDatabase;

        public SqlDatabaseAttribute(SqlDatabase sqlDatabase)
        {
            SqlDatabase = sqlDatabase;
        }
    }

    public class ForeignKeyMapAttribute : Attribute
    {
        public readonly PropertyMap PropertyMap;

        public ForeignKeyMapAttribute(Type modelType, string propertyName)
        {
            PropertyMap = new PropertyMap(modelType, propertyName);
        }
    }
}