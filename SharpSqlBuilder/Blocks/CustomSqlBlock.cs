using System.Linq;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    ///     Custom Sql Block
    /// </summary>
    public class CustomSqlBlock : CollectionBlock<SqlCustomSqlBlock>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var joins = Entities.Select(e => e.BuildSql(sqlOptions, FlowOptions.Construct(this)));
            return string.Join(sqlOptions.NewLine(), joins);
        }
    }
}