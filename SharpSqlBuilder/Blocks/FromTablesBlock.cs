using System.Linq;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>FROM ..., ..., ...</example>
    /// </summary>
    public class FromTablesBlock : CollectionBlock<TableEntity>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var tables = string.Join(", ", Entities.Select(e => e.BuildSql(sqlOptions, FlowOptions.Construct(this))));
            var command = sqlOptions.Command("FROM");
            return $"{command} {tables}";
        }
    }
}