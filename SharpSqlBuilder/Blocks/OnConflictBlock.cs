using System.Linq;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Entities;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>ON CONFLICT ..., ..., ...</example>
    /// </summary>
    public class OnConflictBlock : CollectionBlock<ColumnEntity>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var keys = string.Join(", ", Entities.Select(e => e.BuildSql(sqlOptions)));
            string command;
            switch (sqlOptions.DatabaseType)
            {
                case SqlDatabaseType.Postgres:
                case SqlDatabaseType.SqLite:
                    command = "ON CONFLICT";
                    return $"{command} ({keys})";
                case SqlDatabaseType.MySql:
                case SqlDatabaseType.MariaDb:
                    command = "ON DUPLICATE KEY";
                    return $"{command}";
                default: return null;
            }
        }
    }
}