using System.Linq;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>RETURNING ... AS ..., ... AS ...</example>
    /// </summary>
    public class ReturningBlock : CollectionBlock<ColumnAsPropertyEntity>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var keys = string.Join($",{sqlOptions.NewLine()}{sqlOptions.Indent()}", Entities.Select(e => e.BuildSql(sqlOptions, FlowOptions.Construct(this))));
            var command = "RETURNING";
            return $"{command}{sqlOptions.NewLine()}{sqlOptions.Indent()}{keys}";
        }
    }
}