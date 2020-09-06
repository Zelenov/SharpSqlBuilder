using SharpSqlBuilder.Attributes;

namespace SharpSqlBuilder.Builders
{
    public enum SqlDialect
    {
        [SqlDatabase(SqlDatabase.Postgres)] Postgres95,
        [SqlDatabase(SqlDatabase.Postgres)] MsSql12,
        [SqlDatabase(SqlDatabase.Postgres)] SqLite30,
        [SqlDatabase(SqlDatabase.Postgres)] MySql57,
        [SqlDatabase(SqlDatabase.Postgres)] MariaDb102
    }
}