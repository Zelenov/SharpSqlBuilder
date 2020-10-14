using System.Linq;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>DO UPDATE SET ... = ..., ... = ...</example>
    /// </summary>
    public class DoUpdateBlock : CollectionBlock<OnConflictUpdateValueBlock>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var properties = string.Join($",{sqlOptions.NewLine()}{sqlOptions.Indent()}",
                Entities.Select(e => e.BuildSql(sqlOptions, FlowOptions.Construct(this))));
            string command;
            switch (sqlOptions.DatabaseType)
            {
                case SqlDatabaseType.Postgres:
                case SqlDatabaseType.SqLite:
                    command = "DO UPDATE SET";
                    break;
                case SqlDatabaseType.MySql:
                case SqlDatabaseType.MariaDb:
                    command = "UPDATE";
                    break;
                default: return null;
            }

            return $"{command}{sqlOptions.NewLine()}{sqlOptions.Indent()}{properties}";
        }
    }
}