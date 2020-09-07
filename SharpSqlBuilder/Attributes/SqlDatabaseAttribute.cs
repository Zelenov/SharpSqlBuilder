using System;
using SharpSqlBuilder.Builders;

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
}