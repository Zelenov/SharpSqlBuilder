using System.Linq;
using SharpSqlBuilder.Entities;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>RETURNING ..., ..., ...</example>
    /// </summary>
    public class ReturningBlock : CollectionBlock<ColumnEntity>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var keys = string.Join(", ", Entities.Select(e => e.BuildSql(sqlOptions)));
            var command = "RETURNING";
            return $"{command} {keys}";
        }
    }
}