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
            switch (sqlOptions.Database)
            {
                case SqlDatabase.Postgres:
                case SqlDatabase.SqLite:
                    command = "ON CONFLICT";
                    return $"{command} ({keys})";
                case SqlDatabase.MySql:
                case SqlDatabase.MariaDb:
                    command = "ON DUPLICATE KEY";
                    return $"{command}";
                default: return null;
            }
        }
    }
}