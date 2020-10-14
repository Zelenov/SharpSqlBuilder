using System.Linq;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>(..., ..., ...)</example>
    /// </summary>
    public class InsertColumnsBlock : CollectionBlock<ColumnEntity>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var columns = string.Join(", ", Entities.Select(e => e.BuildSql(sqlOptions, FlowOptions.Construct(this))));
            return $"({columns})";
        }
    }
}