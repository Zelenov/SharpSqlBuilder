using System.Linq;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;

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
                Entities.Select(e => e.BuildSql(sqlOptions)));
            string command;
            switch (sqlOptions.Database)
            {
                case SqlDatabase.Postgres:
                case SqlDatabase.SqLite:
                    command = "DO UPDATE SET";
                    break;
                case SqlDatabase.MySql:
                case SqlDatabase.MariaDb:
                    command = "UPDATE";
                    break;
                default: return null;
            }

            return $"{command}{sqlOptions.NewLine()}{sqlOptions.Indent()}{properties}";
        }
    }
}