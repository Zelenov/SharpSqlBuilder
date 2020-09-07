using System;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Attributes
{
    public class SqlDatabaseAttribute : Attribute
    {
        public readonly SqlDatabaseType SqlDatabaseType;

        public SqlDatabaseAttribute(SqlDatabaseType sqlDatabaseType)
        {
            SqlDatabaseType = sqlDatabaseType;
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