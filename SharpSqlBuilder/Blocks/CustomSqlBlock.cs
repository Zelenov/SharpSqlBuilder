using System.Linq;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    ///     Custom Sql Block
    /// </summary>
    public class CustomSqlBlock : CollectionBlock<SqlCustomSqlBlock>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var joins = Entities.Select(e => e.BuildSql(sqlOptions));
            return string.Join(sqlOptions.NewLine(), joins);
        }
    }
}