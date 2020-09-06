using System.Linq;
using SharpSqlBuilder.Entities;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>(..., ..., ...)</example>
    /// </summary>
    public class InsertColumnsBlock : CollectionBlock<ColumnEntity>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var columns = string.Join(", ", Entities.Select(e => e.BuildSql(sqlOptions)));
            return $"({columns})";
        }
    }
}