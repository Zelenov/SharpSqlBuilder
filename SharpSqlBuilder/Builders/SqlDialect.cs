using SharpSqlBuilder.Attributes;

namespace SharpSqlBuilder.Builders
{
    public enum SqlDialect
    {
        [SqlDatabase(SqlDatabaseType.Postgres)] Postgres95,
        [SqlDatabase(SqlDatabaseType.Postgres)] MsSql12,
        [SqlDatabase(SqlDatabaseType.Postgres)] SqLite30,
        [SqlDatabase(SqlDatabaseType.Postgres)] MySql57,
        [SqlDatabase(SqlDatabaseType.Postgres)] MariaDb102
    }
}